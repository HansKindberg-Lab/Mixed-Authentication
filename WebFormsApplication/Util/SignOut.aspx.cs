using System;
using Shared.InversionOfControl;
using Shared.Web.Security;

namespace WebFormsApplication.Util
{
	public partial class SignOut : AuthenticationPage
	{
		#region Constructors

		public SignOut() : this(ServiceLocator.Instance.GetService<IFormsAuthentication>()) {}
		public SignOut(IFormsAuthentication formsAuthentication) : base(formsAuthentication) {}

		#endregion

		#region Methods

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if(this.User.Identity.IsAuthenticated)
			{
				this.FormsAuthentication.SignOut();

				this.Response.Redirect(this.Request.RawUrl);
			}
		}

		#endregion
	}
}