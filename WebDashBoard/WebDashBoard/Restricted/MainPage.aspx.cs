using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

namespace WebDashBoard
{
    public partial class MainPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UseridCookie"] != null)
            {
                Label1.Text = Request.Cookies["UseridCookie"].Value;
            }
            else
            {
                Label1.Text = "There was nothing here....";
            }
            if (Request.Cookies["membercookie"] != null)
            {
                Label2.Text = Request.Cookies["membercookie"].ToString();
            }
            else
            {
                Label2.Text = "There was nothing here....";
            }
        }
        protected void btnDaHaus_Click(object sender, EventArgs e)
        {
            Response.Redirect("DaHaus.aspx");
        }

        protected void btnGames_Click(object sender, EventArgs e)
        {
            Response.Redirect("Games.aspx");
        }

        protected void btnSites_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sites.aspx");
        }

        protected void btnOpties_Click(object sender, EventArgs e)
        {
            Response.Redirect("Opties.aspx");
        }

        protected void btnSteam_Click(object sender, EventArgs e)
        {
            Response.Redirect("Steam.aspx");
        }

        protected void btnAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("Account.aspx");
        }
    }
}