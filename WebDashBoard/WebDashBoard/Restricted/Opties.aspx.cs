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
            if (!IsPostBack)
            {
                mvwOpties.Visible = false;
            }
            lblUserid.Text = Session["Userid"].ToString();
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

        protected void btnSites_Click(object sender, EventArgs e)
        {
            mvwOpties.Visible = true;
            mvwOpties.ActiveViewIndex = 0;
        }

        protected void btnGames_Click(object sender, EventArgs e)
        {
            mvwOpties.Visible = true;
            mvwOpties.ActiveViewIndex = 1;
        }

        protected void ddlWebsites_SelectedIndexChanged(object sender, EventArgs e)
        {
            fvwAddsites.PageIndex = ddlWebsites.SelectedIndex;
        }
    }
}