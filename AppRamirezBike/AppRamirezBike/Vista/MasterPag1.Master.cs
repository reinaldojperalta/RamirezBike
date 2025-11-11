using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
	public partial class Site1 : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
	          if(Context.User != null && Context.User.Identity.IsAuthenticated)
			{
				btnLogout.Visible = true;

			}
			else
			{
				btnLogout.Visible = false;
			}

		}

        protected void btnLogout_Click(object sender, EventArgs e)
        {
			FormsAuthentication.SignOut();

			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
			cookie.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie);


			Response.Redirect("/Vista/Login.aspx");

        }
    }
}