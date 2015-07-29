using System;
using System.Web;
using MvcApplication.Models.Forms;

namespace MvcApplication.Models.ViewModels
{
	public class AuthenticationViewModel
	{
		#region Fields

		private AuthenticationFormModel _form;
		private readonly HttpRequestBase _httpRequest;
		private Uri _signInWithWindowsIdentityUrl;

		#endregion

		#region Constructors

		public AuthenticationViewModel(HttpRequestBase httpRequest)
		{
			if(httpRequest == null)
				throw new ArgumentNullException("httpRequest");

			this._httpRequest = httpRequest;
		}

		#endregion

		#region Properties

		public virtual AuthenticationFormModel Form
		{
			get { return this._form ?? (this._form = new AuthenticationFormModel()); }
			set { this._form = value; }
		}

		protected internal virtual HttpRequestBase HttpRequest
		{
			get { return this._httpRequest; }
		}

		public virtual Uri SignInWithWindowsIdentityUrl
		{
			get
			{
				if(this._signInWithWindowsIdentityUrl == null)
				{
					// ReSharper disable AssignNullToNotNullAttribute
					var uriBuilder = new UriBuilder(this.HttpRequest.Url);
					// ReSharper restore AssignNullToNotNullAttribute

					uriBuilder.Path = "/Authentication/SignInWithWindowsIdentity/";

					this._signInWithWindowsIdentityUrl = uriBuilder.Uri;
				}

				return this._signInWithWindowsIdentityUrl;
			}
		}

		#endregion
	}
}