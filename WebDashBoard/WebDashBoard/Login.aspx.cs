using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

namespace WebDashBoard
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void lgnLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            /*if (IsValid)
            {
                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DashboardDB"].ToString();
                string Username;
                int Userid = 10;
                Username = User.Identity.Name.ToString();
                OleDbCommand UseridCmd = new OleDbCommand();
                UseridCmd.CommandText = "SELECT Userid FROM Gebruiker WHERE Gebruikersnaam = ?";
                OleDbParameter param = new OleDbParameter();
                param.Value = Username;
                UseridCmd.Parameters.Add(param);
                UseridCmd.Connection = conn;
                OleDbDataReader rdr = UseridCmd.ExecuteReader();
                using (conn)
                {
                    using (rdr)
                    {
                        while (rdr.Read())
                        {
                            Userid = int.Parse(rdr.GetValue(0).ToString());
                            HttpCookie UseridCookie = new HttpCookie("UseridCookie");
                            UseridCookie.Values["Userid"] = Userid.ToString();
                            UseridCookie.Expires = DateTime.Now.AddDays(10.0);
                            Response.Cookies.Add(UseridCookie);
                        }
                    }
                }
            }*/
        }
        
    }
}