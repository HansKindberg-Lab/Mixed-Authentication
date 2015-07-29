using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Security;
using MvcApplication.Models.Forms;
using MvcApplication.Models.ViewModels;

namespace MvcApplication.Controllers
{
	public class AuthenticationController : Controller
	{
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

			FormsAuthentication.SetAuthCookie(string.IsNullOrWhiteSpace(form.UserName) ? Shared.Global.TheUserName : form.UserName, form.Persist);

			return this.Redirect(this.GetReturnUrl());
		}

		public virtual ActionResult SignInWithWindowsIdentity()
		{
			var logonUser = this.Request.ServerVariables["LOGON_USER"];

			if(!string.IsNullOrEmpty(logonUser))
				FormsAuthentication.SetAuthCookie(logonUser, true);

			return this.Redirect(this.GetReturnUrl());
		}

		public virtual ActionResult SignOut()
		{
			if(this.User.Identity.IsAuthenticated)
			{
				FormsAuthentication.SignOut();

				return this.Redirect(this.Request.RawUrl);
			}

			return this.View();
		}

		#endregion
	}
}