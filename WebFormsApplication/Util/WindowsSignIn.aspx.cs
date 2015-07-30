using System;
using System.Security.Principal;
using Shared.InversionOfControl;
using Shared.Web.Security;

namespace WebFormsApplication.Util
{
	public partial class WindowsSignIn : AuthenticationPage
	{
		#region Constructors

		public WindowsSignIn() : this(ServiceLocator.Instance.GetService<IFormsAuthentication>()) {}
		public WindowsSignIn(IFormsAuthentication formsAuthentication) : base(formsAuthentication) {}

		#endregion

		#region Methods

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			var user = this.User;

			// ReSharper disable InvertIf
			if(user != null && user.Identity.IsAuthenticated && user.Identity is WindowsIdentity)
			{
				var windowsUserName = user.Identity.Name;

				if(string.IsNullOrEmpty(windowsUserName))
					throw new InvalidOperationException("The user-identity-name can not be null or empty.");

				this.FormsAuthentication.SetAuthenticationCookie(windowsUserName, true);
			}
			// ReSharper restore InvertIf

			this.Response.Redirect(this.GetReturnUrl());
		}

		#endregion
	}
}