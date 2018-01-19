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
    public partial class Opties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Username = User.Identity.Name.ToString();
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DashboardDB"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception exc)
                {

                }
                finally
                {
                    conn.Close();
                }

                mvwOpties.ActiveViewIndex = 0;
                lblUsername.Text = Username;
            }
        }

        protected void btnSites_Click(object sender, EventArgs e)
        {
            mvwOpties.ActiveViewIndex = 1;
        }

        protected void btnGames_Click(object sender, EventArgs e)
        {
            mvwOpties.ActiveViewIndex = 2;
        }

        protected void btnSitesAdd_Click(object sender, EventArgs e)
        {
            mvwOpties.ActiveViewIndex = 3;
        }

        protected void btnReturnG_Click(object sender, EventArgs e)
        {
            mvwOpties.ActiveViewIndex = 0;
        }

        protected void btnReturnAS_Click(object sender, EventArgs e)
        {
            mvwOpties.ActiveViewIndex = 1;
        }

        protected void btnReturnA_Click(object sender, EventArgs e)
        {
            mvwOpties.ActiveViewIndex = 0;
        }

        protected void btnSitesDisplay_Click(object sender, EventArgs e)
        {

            /*OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DashboardDB"].ToString();

            OleDbCommand Insertcmd = new OleDbCommand();
            Insertcmd.Connection = conn;
            Insertcmd.CommandText = "INSERT INTO Mainweb(Userid, Webid) VALUES (?, ?)";
            Insertcmd.Parameters.Add("Userid", OleDbType.VarChar).Value = lblUserid.Text;
            Insertcmd.Parameters.Add("Webid", OleDbType.VarChar).Value = lblWebid.Text;
            
            try
            {
                conn.Open();
                Insertcmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {

            }
            finally
            {
                conn.Close();
            }*/
        }
    }
}