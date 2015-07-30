using System;
using Shared.InversionOfControl;
using Shared.Web.Security;

namespace WebFormsApplication.Util
{
	public partial class SignIn : AuthenticationPage
	{
		#region Fields

		private Uri _windowsSignInUrl;

		#endregion

		#region Constructors

		public SignIn() : this(ServiceLocator.Instance.GetService<IFormsAuthentication>()) {}
		public SignIn(IFormsAuthentication formsAuthentication) : base(formsAuthentication) {}

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
			this.FormsAuthentication.SetAuthenticationCookie(string.IsNullOrWhiteSpace(this.UserNameTextBox.Text) ? Shared.Global.TheUserName : this.UserNameTextBox.Text, this.PersistCheckBox.Checked);

			this.Response.Redirect(this.GetReturnUrl());
		}

		#endregion
	}
}