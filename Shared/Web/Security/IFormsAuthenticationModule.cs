using System;
using System.Web;
using System.Web.Security;

namespace Shared.Web.Security
{
	public interface IFormsAuthenticationModule : IHttpModule
	{
		#region Events

		event EventHandler<FormsAuthenticationEventArgs> Authenticate;

		#endregion

		#region Properties

		bool Enabled { get; set; }

		#endregion

		#region Methods

		void OnAuthenticateRequest(object sender, EventArgs e);
		void OnEndRequest(object sender, EventArgs e);

		#endregion
	}
}