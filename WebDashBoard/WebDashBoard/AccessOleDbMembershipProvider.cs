﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;              // base class of MembershipProvider is in this namespace
using System.Data.OleDb;                // for working with Access OleDb databases (e.g. OleDbCommand)
using System.Configuration.Provider;    // ProviderException class
using System.Configuration;             // web.config ConnectionStringSettings and ConfiguratioManager classes
using System.Security.Cryptography;     // RNGCryptoServiceProvider & SHA256Managed classes
using System.Text;                      // Encoding, UTF8Encoding & StringBuilder classes
using System.Data;                      // CommandBehavior enumeration

/*
This provider works with the following schema for the table of user data.
The table can contain more fields than just these - these are the ones that
are mandatory. If you want to change this, you'll have to change the
behavior of some methods in this class, especially the SQL queries.
This data definition query was tested with Access 2010:

CREATE TABLE users
(
  id AUTOINCREMENT NOT NULL PRIMARY KEY,
  username Text (128) NOT NULL UNIQUE,
  password Text (128) NOT NULL,
  emailaddress Text (128) NOT NULL UNIQUE
)

Mind you: when querying for a field named 'password', you're actually using
a reserved keyword in Access! ALWAYS surround this particular field with
square brackets when calling on it explicitly in a query te prevent problems,
like so: [password]

Web.config configuration attributes:
 * applicationName (defaults to System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath) 
 * passwordAttemptWindow (defaults to 10)
 * minRequiredNonAlphanumericCharacters (defaults to 0)
 * minRequiredPasswordLength (defaults to 6)
 * passwordStrengthRegularExpression (defaults to "", which equals no pregmatch evaluation)
 * enablePasswordReset (defaults to true)
 * requiresUniqueEmail (defaults to true)
 * connectionStringName (mandatory attribute that should be explicitly 
   provided as a valid connectionstring in Web.config)

Web.config and similar attributes that cannot be set (hardcoded in this class):
 * maxInvalidPasswordAttempts = Int32.MaxValue (no lockout mechanism implemented)
 * requiresQuestionAndAnswer  = false;
 * enablePasswordRetrieval    = false;
 * passwordFormat             = MembershipPasswordFormat.Hashed;
 * newPasswordLength          = 8;

The Q&A thing is not implemented, because password recovery cannot be done when
the password is saved as salt + hash. For hashing, SHA256 is used. The salt is
256 bits long and appropriately generated by RNGCryptoServiceProvider. For most
real-life business applications, this is already more than enough (although
using Access might not be...). If you want enterprise-grade password hashing,
look into something like bcrypt.

If you want to change any of the default values of the configuration attributes
above, you'll have to do that in the 'Initialize()' method
below. Most of them require you to do additional implementation of methods
that are prototyped here, but not factually overrided yet. For example,
if you want requiresQuestionAndAnswer to be true, you should implement
ChangePasswordQuestionAndAnswer(), and modify your database schema accordingly.
*/

namespace CustomMembership
{
    public sealed class AccessOleDbMembershipProvider : MembershipProvider
    {
        //Password length of generated passwords
        private int newPasswordLength;
        //Minimun password length
        private int minRequiredPasswordLength;
        //Minimum non-alphanumeric char required
        private int minRequiredNonAlphanumericCharacters;
        //Enable - disable password retrieval
        private bool enablePasswordRetrieval;
        //Enable - disable password resetting
        private bool enablePasswordReset;
        //Require security question and answer
        private bool requiresQuestionAndAnswer;
        //Application name
        private string applicationName;
        //Max number of failed password attempts before the account is blocked
        private int maxInvalidPasswordAttempts;
        //Time to reset counter for 'maxInvalidPasswordAttempts', in minutes
        private int passwordAttemptWindow;    
        //Password format
        private MembershipPasswordFormat passwordFormat;
        //Regular expression the password should match (empty for none)
        private string passwordStrengthRegularExpression;
        //Class-level placeholder for web.config's connectionstring
        private string connectionString;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            //
            // Initialize values from Web.config
            //
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "KortereNaam";

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Project- and database specific Access OleDb Membership provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);
            
            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "0"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "6"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], string.Empty));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));

            // Configuration items that are not supported or have, in this
            // membership implementation, only one possible value, are set
            // appropriately here, to avoid any confusion later on. One could
            // also refer to these as the 'hard-coded' settings:
            newPasswordLength = 8;
            //maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "3"));
            maxInvalidPasswordAttempts = Int32.MaxValue;
            //requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresQuestionAndAnswer = false;
            //enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "false"));
            enablePasswordRetrieval = false;
            // We're using good ol' hash + salt for our passwords:
            passwordFormat = MembershipPasswordFormat.Hashed;

            //
            // Initialize OleDbConnection for use with an Access Database:
            //
            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];
            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank. Check the <add> tag's connectionStringName attrib in Web.config under membership -> providers...");
            }
            connectionString = ConnectionStringSettings.ConnectionString;
        }

        //
        // A helper function to retrieve config values from the configuration file.
        //
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override string ApplicationName
        {
            get { return this.applicationName; }
            set { this.applicationName = value; }
        }

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            // Makes use of the 'ValidateUser()' method:
            if (!ValidateUser(username, oldPwd))
                return false;

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPwd, true);
            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");

            // To test:
            if (newPwd.Length < minRequiredPasswordLength)
            {
                return false;
            }

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand updateCmd = new OleDbCommand("UPDATE Gebruiker SET [Wachtwoord] = ? WHERE Gebruikersnaam = ?", conn);

            string pass = HashPassword(newPwd);
            updateCmd.Parameters.Add("@Wachtwoord", OleDbType.VarChar, 128).Value = pass;
            updateCmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = username;
            int rowsAffected = 0;

            try
            {
                conn.Open();
                rowsAffected = updateCmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            // Not needed, Q&A mechanism is unused by this Provider
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            
            if (args.Cancel || (password.Length < minRequiredPasswordLength))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null) // Insert new into DB only if 'u'ser does not already exist...
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Gebruiker (Gebruikersnaam, [Wachtwoord]) Values(?, ?)", conn);

                string pass = HashPassword(password);
                cmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = username;
                cmd.Parameters.Add("@Wachtwoord", OleDbType.VarChar, 128).Value = pass;
                //cmd.Parameters.Add("@Vraag", OleDbType.VarChar, 255).Value = passwordQuestion;
                //cmd.Parameters.Add("@Reminder", OleDbType.VarChar, 255).Value = passwordAnswer;

                try
                {
                    conn.Open();

                    int recAdded = cmd.ExecuteNonQuery();

                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch(Exception ex)
                {
                    string bok = ex.Message;
                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    conn.Close();
                }
                
                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            // Needed if you manage user table via WAT
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Gebruiker WHERE Gebruikersnaam = ?", conn);

            cmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = username;

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (deleteAllRelatedData)
                {
                    // Process commands to delete all data for the user in the database.
                    // For example, you can delete files, like images, here
                    // from the filesystem if necessary...
                    //
                    // Specifically: delete usersinroles data that was added
                    // to the usersinroles table. Should only work if the 
                    // 'AccessOleDbRoleProvider' is used, i.e. if the
                    // 'usersinroles' table exists...
                    string[] restrictionValues = new string[4] { null, null, null, "TABLE" };
                    bool tblExists = false; 
                    DataTable schemaInformation = conn.GetSchema("Tables", restrictionValues);
                    foreach (DataRow row in schemaInformation.Rows)
                    {
                        if (row.ItemArray[2].ToString() == "usersinroles") // if table named usersinroles exists...
                        {
                            tblExists = true;
                            break;
                        }
                    }

                    if (tblExists)
                    {
                        cmd.CommandText = "DELETE FROM usersinroles WHERE username = ?"; // parameter stays the same as before...
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT Count(*) FROM Gebruiker WHERE Gebruikersnaam LIKE ?", conn);
            cmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = "%" + usernameToMatch + "%";
            MembershipUserCollection users = new MembershipUserCollection();
            OleDbDataReader reader = null;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();
                if (totalRecords <= 0) { return users; }
                cmd.CommandText = "SELECT Userid, Gebruikersnaam FROM Gebruiker WHERE Gebruikersnaam LIKE ? ORDER BY Gebruikersnaam ASC";
                // Can we just leave the cmd.Parameters collection untouched
                // while changing the query and expect the DBMS to remap the
                // same parameters to a different query? I guess so!
                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = new MembershipUser(this.Name,
                                                reader.GetString(1), // username from DB
                                                reader.GetValue(0), // id from DB
                                                reader.GetString(2), //emailaddress from DB
                                                string.Empty, // no password question
                                                string.Empty, // no comment
                                                true, // approved
                                                false, // not locked out
                                                System.DateTime.MinValue,
                                                System.DateTime.MinValue,
                                                System.DateTime.MinValue,
                                                System.DateTime.MinValue,
                                                System.DateTime.MinValue);
                        users.Add(u);
                    }
                    if (counter >= endIndex) { cmd.Cancel(); }
                    counter++;
                }
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return users;
        }
        
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            // Needed for displaying all users in WAT
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT Count(*) FROM Gebruiker", conn);
            MembershipUserCollection users = new MembershipUserCollection();
            OleDbDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();
                if (totalRecords <= 0) { return users; }
                cmd.CommandText = "SELECT Userid, Gebruikersnaam FROM Gebruiker ORDER BY Gebruikersnaam ASC";
                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = new MembershipUser(this.Name, // ggProviderBase.Name is the friendly name used to refer to the Provider during configuration
                                                reader.GetString(1), // username from DB
                                                reader.GetValue(0), // id from DB
                                                reader.GetString(2), //emailaddress from DB
                                                string.Empty, // no password question
                                                string.Empty, // no comment
                                                true, // approved
                                                false, // not locked out
                                                System.DateTime.MinValue, // no creation date stored
                                                System.DateTime.MinValue, // no last login date stored
                                                System.DateTime.MinValue, // no last activity date stored
                                                System.DateTime.MinValue, // no last password change date stored
                                                System.DateTime.MinValue); // no last locked out date stored
                        users.Add(u);
                    }
                    if (counter >= endIndex) { cmd.Cancel(); }
                    counter++;
                }
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            // Not needed, not storing field 'LastActivityDate' in DB, so no
            // way to R/W whether the user is online right now.
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            // Not needed, password is 'one-way' hashed and non-retrievable
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT Userid, Gebruikersnaam FROM Gebruiker WHERE Gebruikersnaam = ?", conn);
            cmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = username;
            MembershipUser u = null;
            OleDbDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    reader.Read();
                    u = new MembershipUser( this.Name, // ProviderBase.Name is the friendly name used to refer to the Provider during configuration
                                            reader.GetString(1), // username from DB
                                            reader.GetValue(0), // id from DB
                                            reader.GetString(2),
                                            string.Empty, // no password question
                                            string.Empty, // no comment
                                            true, // approved
                                            false, // not locked out
                                            System.DateTime.MinValue, // no creation date stored
                                            System.DateTime.MinValue, // no last login date stored
                                            System.DateTime.MinValue, // no last activity date stored
                                            System.DateTime.MinValue, // no last password change date stored
                                            System.DateTime.MinValue); // no last locked out date stored
                }
                reader.Close();
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }
            return u;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            // Not needed
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        // Returns the newly generated password. 'answer' parameter is ignored, Q&A system is not implemented
        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            string newPassword = System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand updateCmd = new OleDbCommand("UPDATE Gebruiker SET [Wachtwoord] = ? WHERE Gebruikersnaam = ?", conn);

            string pass = HashPassword(newPassword);
            updateCmd.Parameters.Add("@Wachtwoord", OleDbType.VarChar, 128).Value = pass;
            updateCmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = username;
            int rowsAffected = 0;

            try
            {
                conn.Open();
                rowsAffected = updateCmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found. Password not Reset.");
            }
        }

        public override bool UnlockUser(string userName)
        {
            // Not needed, user can't get locked out and we're not keeping any
            // boolean regarding being locked out in the user table in the DB:
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            // Get both Hash + Salt from DB and do some comparing using
            // the utility functions...
            bool isValid = false;

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("SELECT [Wachtwoord] FROM Gebruiker WHERE Gebruikersnaam = ?", conn);
            cmd.Parameters.Add("@Gebruikersnaam", OleDbType.VarChar, 40).Value = username;

            OleDbDataReader reader = null;
            string pwd = "";

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();
                    pwd = reader.GetString(0);
                }
                else
                {
                    return false;
                }

                reader.Close();

                if (CheckPassword(password, pwd))
                {
                    isValid = true;
                }
            }
            catch (OleDbException e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return isValid;
        }


        //
        // Password encryption & utility functions
        //
        
        // Compares password values based on Hashing (salt included in correctHash)
        // This method is used directly by public methods of this MembershipProvider
        private bool CheckPassword(string password, string correctHash)
        {
            if (correctHash.Length < 128)
            {
                throw new ArgumentException("correctHash must be 128 hex characters!");
            }
            string salt = correctHash.Substring(0, 64);
            string validHash = correctHash.Substring(64, 64);
            string passHash = Sha256Hex(salt + password);
            return string.Compare(validHash, passHash) == 0;
        }
        
        // Returns the hashed password as a 128 character hex string
        // This method is used directly by public methods of this MembershipProvider
        private string HashPassword(string password)
        {
            string salt = GetRandomSalt(); // generate a new random salt
            string hash = Sha256Hex(salt + password); // hashing: first salt, then password
            return salt + hash; // concatenation: first 64 bytes salt, then 64 bytes hashof(salt + password)
        }

        // Returns the SHA256 hash of a string, formatted in hex
        // This method is a utility function used only by other encryption methods
        private string Sha256Hex(string toHash)
        {
            SHA256Managed hash = new SHA256Managed();
            byte[] utf8 = UTF8Encoding.UTF8.GetBytes(toHash);
            return BytesToHex(hash.ComputeHash(utf8));
        }

        // Returns a random 64 character hex string, used as the ever-unique hash salt (256 bits)
        // This method is a utility function used only by other encryption methods
        private string GetRandomSalt()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[32]; //256 bits
            random.GetBytes(salt);
            return BytesToHex(salt); // returns a 64 byte (512 bit) string, see below
        }

        // Converts a byte array to a hex string
        // This method is a utility function used only by other encryption methods
        private string BytesToHex(byte[] toConvert)
        {
            StringBuilder s = new StringBuilder(toConvert.Length * 2); // 32 * 2 = 64 bytes
            foreach (byte b in toConvert)
            {
                // "x2" specifies a hexadecimal string with 2 digits for each byte
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
            // resulting string is twice the size of the byte array (32 bytes input yields s.Length == 64)
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
    }
}