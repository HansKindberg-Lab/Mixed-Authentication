using System;
using System.Web.Security;
using System.Web.UI;

namespace WebFormsApplication.Util
{
	public partial class WindowsSignIn : Page
	{
		#region Methods

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			var logonUser = this.Request.ServerVariables["LOGON_USER"];

			if(!string.IsNullOrEmpty(logonUser))
				FormsAuthentication.SetAuthCookie(logonUser, true);

			var returnUrl = this.Request.QueryString["ReturnUrl"];

			if(string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			this.Response.Redirect(returnUrl);
		}

		#endregion
	}
}