using System;
using System.Web.Security;
using System.Web.UI;

namespace WebFormsApplication.Util
{
	public partial class SignOut : Page
	{
		#region Methods

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if(this.User.Identity.IsAuthenticated)
			{
				FormsAuthentication.SignOut();

				this.Response.Redirect(this.Request.RawUrl);
			}
		}

		#endregion
	}
}