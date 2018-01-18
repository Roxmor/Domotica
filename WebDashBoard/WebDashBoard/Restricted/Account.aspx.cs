using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDashBoard
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvwAccount.ActiveViewIndex = 0;
            }
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            mvwAccount.ActiveViewIndex = 1;
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            mvwAccount.ActiveViewIndex = 0;
        }
    }
}