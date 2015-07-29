using System;
using System.Web;
using System.Web.Security;

namespace Shared.Web.Security
{
	public interface IFormsAuthenticationModule
	{
		#region Events

		event EventHandler<FormsAuthenticationEventArgs> Authenticate;

		#endregion

		#region Properties

		bool FormsAuthenticationRequired { get; set; }

		#endregion

		#region Methods

		void Dispose();
		void Init(HttpApplication context);
		void OnAuthenticateRequest(object sender, EventArgs e);
		void OnEndRequest(object sender, EventArgs e);

		#endregion
	}
}