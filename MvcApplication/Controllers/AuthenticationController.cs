using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Mvc;
using MvcApplication.Models.Forms;
using MvcApplication.Models.ViewModels;
using Shared.Web.Security;

namespace MvcApplication.Controllers
{
	public class AuthenticationController : Controller
	{
		#region Fields

		private readonly IFormsAuthentication _formsAuthentication;

		#endregion

		#region Constructors

		public AuthenticationController(IFormsAuthentication formsAuthentication)
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

		protected internal virtual AuthenticationViewModel CreateModel()
		{
			return this.CreateModel(null);
		}

		protected internal virtual AuthenticationViewModel CreateModel(AuthenticationFormModel form)
		{
			return new AuthenticationViewModel(this.Request)
			{
				Form = form
			};
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
		protected internal virtual string GetReturnUrl()
		{
			var returnUrl = this.HttpContext.Request.QueryString["ReturnUrl"];

			if(string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";

			return returnUrl;
		}

		public virtual ActionResult Index()
		{
			return this.View(this.CreateModel());
		}

		[HttpPost]
		public virtual ActionResult Index(AuthenticationFormModel form)
		{
			if(form == null)
				throw new ArgumentNullException("form");

			this.FormsAuthentication.SetAuthenticationCookie(string.IsNullOrWhiteSpace(form.UserName) ? Shared.Global.TheUserName : form.UserName, form.Persist);

			return this.Redirect(this.GetReturnUrl());
		}

		public virtual ActionResult SignInWithWindowsIdentity()
		{
			var user = this.HttpContext.User;

			// ReSharper disable InvertIf
			if(user != null && user.Identity.IsAuthenticated && user.Identity is WindowsIdentity)
			{
				var windowsUserName = user.Identity.Name;

				if(string.IsNullOrEmpty(windowsUserName))
					throw new InvalidOperationException("The user-identity-name can not be null or empty.");

				this.FormsAuthentication.SetAuthenticationCookie(windowsUserName, true);
			}
			// ReSharper restore InvertIf

			return this.Redirect(this.GetReturnUrl());
		}

		public virtual ActionResult SignOut()
		{
			if(this.User.Identity.IsAuthenticated)
			{
				this.FormsAuthentication.SignOut();

				return this.Redirect(this.Request.RawUrl);
			}

			return this.View();
		}

		#endregion
	}
}