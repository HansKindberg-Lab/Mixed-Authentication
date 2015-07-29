using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Shared.Configuration;
using Shared.Web.Configuration;

namespace Shared.Web.Security
{
	public class MixedAuthenticationModule : IHttpModule
	{
		#region Fields

		private readonly AuthenticationSection _authenticationSection;
		private readonly IConfigurationManager _configurationManager;
		private readonly IFormsAuthentication _formsAuthentication;
		private readonly IFormsAuthenticationModule _formsAuthenticationModule;

		#endregion

		#region Constructors

		public MixedAuthenticationModule() : this(System.Configuration.ConfigurationManager.GetSection("system.web/authentication") as AuthenticationSection, new ConfigurationManagerWrapper(), new FormsAuthenticationWrapper(), new FormsAuthenticationModuleWrapper(new FormsAuthenticationModule())) {}

		public MixedAuthenticationModule(AuthenticationSection authenticationSection, IConfigurationManager configurationManager, IFormsAuthentication formsAuthentication, IFormsAuthenticationModule formsAuthenticationModule)
		{
			if(authenticationSection == null)
				throw new ArgumentNullException("authenticationSection");

			if(configurationManager == null)
				throw new ArgumentNullException("configurationManager");

			if(formsAuthentication == null)
				throw new ArgumentNullException("formsAuthentication");

			if(formsAuthenticationModule == null)
				throw new ArgumentNullException("formsAuthenticationModule");

			this._authenticationSection = authenticationSection;
			this._configurationManager = configurationManager;
			this._formsAuthentication = formsAuthentication;
			this._formsAuthenticationModule = formsAuthenticationModule;
		}

		#endregion

		#region Events

		public event EventHandler<FormsAuthenticationEventArgs> Authenticate
		{
			add { this.FormsAuthenticationModule.Authenticate += value; }
			remove { this.FormsAuthenticationModule.Authenticate -= value; }
		}

		#endregion

		#region Properties

		protected internal virtual AuthenticationSection AuthenticationSection
		{
			get { return this._authenticationSection; }
		}

		protected internal virtual IConfigurationManager ConfigurationManager
		{
			get { return this._configurationManager; }
		}

		protected internal virtual IFormsAuthentication FormsAuthentication
		{
			get { return this._formsAuthentication; }
		}

		protected internal virtual bool FormsAuthenticationDisabled
		{
			get { return ((MixedAuthenticationSection) this.ConfigurationManager.GetSection("mixedAuthentication")).DisableFormsAuthentication; }
		}

		protected internal virtual IFormsAuthenticationModule FormsAuthenticationModule
		{
			get { return this._formsAuthenticationModule; }
		}

		#endregion

		#region Methods

		public virtual void Dispose() {}

		public virtual void Init(HttpApplication context)
		{
			if(context == null)
				throw new ArgumentNullException("context");

			if(this.AuthenticationSection.Mode != AuthenticationMode.None)
				throw new NotSupportedException("The mixed-authentication-module requires authentication-mode \"None\".");

			this.FormsAuthenticationModule.FormsAuthenticationRequired = true;

			this.FormsAuthentication.Initialize();

			context.AuthenticateRequest += this.OnAuthenticateRequest;
			context.EndRequest += this.OnEndRequest;
		}

		protected internal virtual void OnAuthenticateRequest(object sender, EventArgs e)
		{
			if(this.FormsAuthenticationDisabled)
				return;

			this.FormsAuthenticationModule.OnAuthenticateRequest(sender, e);
		}

		protected internal virtual void OnEndRequest(object sender, EventArgs e)
		{
			if(this.FormsAuthenticationDisabled)
				return;

			this.FormsAuthenticationModule.OnEndRequest(sender, e);
		}

		#endregion
	}
}