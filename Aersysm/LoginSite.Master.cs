using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;


namespace CreditWeb
{
    public partial class LoginSiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblname.Text = ConfigurationManager.AppSettings["HISName"].ToString();
        }
    }
}
