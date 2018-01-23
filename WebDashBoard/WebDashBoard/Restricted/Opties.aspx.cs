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

                lblUsername.Text = Username;
            }
        }
    }
}