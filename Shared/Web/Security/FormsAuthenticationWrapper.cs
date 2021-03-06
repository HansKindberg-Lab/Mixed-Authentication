﻿using System.Web.Security;

namespace Shared.Web.Security
{
	public class FormsAuthenticationWrapper : IFormsAuthentication
	{
		#region Properties

		public virtual bool Enabled
		{
			get { return FormsAuthentication.IsEnabled; }
		}

		#endregion

		#region Methods

		public virtual void Initialize()
		{
			FormsAuthentication.Initialize();
		}

		public virtual void SetAuthenticationCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
		}

		public virtual void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		#endregion
	}
}