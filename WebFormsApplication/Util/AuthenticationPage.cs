using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using Shared.Web.Security;

namespace WebFormsApplication.Util
{
	public abstract class AuthenticationPage : Page
	{
		#region Fields

		private readonly IFormsAuthentication _formsAuthentication;

		#endregion

		#region Constructors

		protected AuthenticationPage(IFormsAuthentication formsAuthentication)
		{
			if(formsAuthentication == null)
				throw new ArgumentNullException("formsAuthentication");

			this._formsAuthentication = formsAuthentication;
		}

		#endregion

		#region Properties

		protected internal virtual IFormsAuthentication FormsAuthentication
		{
			get { return this._formsAuthentication; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
		protected internal virtual string GetReturnUrl()
		{
			var returnUrl = this.Request.QueryString["ReturnUrl"];

			if(string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			return returnUrl;
		}

		#endregion
	}
}