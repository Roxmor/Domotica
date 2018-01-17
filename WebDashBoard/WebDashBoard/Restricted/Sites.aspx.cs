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
    public partial class Sites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }

        protected void gvwSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            fvwSites.PageIndex = gvwSites.SelectedIndex;
        }
    }
}