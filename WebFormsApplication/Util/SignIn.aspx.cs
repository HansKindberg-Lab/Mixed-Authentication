using System;
using System.Web.Security;
using System.Web.UI;
using Shared;

namespace WebFormsApplication.Util
{
	public partial class SignIn : Page
	{
		#region Fields

		private Uri _windowsSignInUrl;

		#endregion

		#region Properties

		protected internal virtual Uri WindowsSignInUrl
		{
			get
			{
				if(this._windowsSignInUrl == null)
				{
					var uriBuilder = new UriBuilder(this.Request.Url)
					{
						Path = "/Util/WindowsSignIn.aspx"
					};

					this._windowsSignInUrl = uriBuilder.Uri;
				}

				return this._windowsSignInUrl;
			}
		}

		#endregion

		#region Methods

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			this.UserInformationPlaceHolder.DataBind();

			this.WindowsSignInPlaceHolder.DataBind();
		}

		protected internal virtual void OnSignInButtonClick(object sender, EventArgs e)
		{
			FormsAuthentication.SetAuthCookie(Global.TheUserName, this.PersistCheckBox.Checked);

			var returnUrl = this.Request.QueryString["ReturnUrl"];

			if(string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			this.Response.Redirect(returnUrl);
		}

		#endregion
	}
}