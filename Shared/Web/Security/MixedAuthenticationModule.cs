using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Shared.Configuration;
using Shared.InversionOfControl;
using Shared.Web.Configuration;

namespace Shared.Web.Security
{
	public class MixedAuthenticationModule : IHttpModule
	{
		#region Fields

		private readonly AuthenticationSection _authenticationSection;
		private readonly IConfigurationManager _configurationManager;
		private static readonly IEnumerable<Type> _disallowedModuleTypes = new[] {typeof(FormsAuthenticationModule)};
		private readonly IFormsAuthentication _formsAuthentication;
		private readonly IFormsAuthenticationModule _formsAuthenticationModule;
		private readonly IHttpRuntime _httpRuntime;

		#endregion

		#region Constructors

		public MixedAuthenticationModule() : this(ServiceLocator.Instance.GetService<AuthenticationSection>(), ServiceLocator.Instance.GetService<IConfigurationManager>(), ServiceLocator.Instance.GetService<IFormsAuthentication>(), ServiceLocator.Instance.GetService<IFormsAuthenticationModule>(), ServiceLocator.Instance.GetService<IHttpRuntime>()) {}

		public MixedAuthenticationModule(AuthenticationSection authenticationSection, IConfigurationManager configurationManager, IFormsAuthentication formsAuthentication, IFormsAuthenticationModule formsAuthenticationModule, IHttpRuntime httpRuntime)
		{
			if(authenticationSection == null)
				throw new ArgumentNullException("authenticationSection");

			if(configurationManager == null)
				throw new ArgumentNullException("configurationManager");

			if(formsAuthentication == null)
				throw new ArgumentNullException("formsAuthentication");

			if(formsAuthenticationModule == null)
				throw new ArgumentNullException("formsAuthenticationModule");

			if(httpRuntime == null)
				throw new ArgumentNullException("httpRuntime");

			this._authenticationSection = authenticationSection;
			this._configurationManager = configurationManager;
			this._formsAuthentication = formsAuthentication;
			this._formsAuthenticationModule = formsAuthenticationModule;
			this._httpRuntime = httpRuntime;
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

		protected internal virtual IEnumerable<Type> DisallowedModuleTypes
		{
			get { return _disallowedModuleTypes; }
		}

		protected internal virtual IFormsAuthentication FormsAuthentication
		{
			get { return this._formsAuthentication; }
		}

		protected internal virtual bool FormsAuthenticationDisabled
		{
			get { return ((MixedAuthenticationSection) this.ConfigurationManager.GetSection("mixedAuthentication")).FormsAuthenticationDisabled; }
		}

		protected internal virtual IFormsAuthenticationModule FormsAuthenticationModule
		{
			get { return this._formsAuthenticationModule; }
		}

		protected internal virtual IHttpRuntime HttpRuntime
		{
			get { return this._httpRuntime; }
		}

		#endregion

		#region Methods

		public virtual void Dispose() {}

		public virtual void Init(HttpApplication context)
		{
			if(context == null)
				throw new ArgumentNullException("context");

			if(!this.HttpRuntime.UsingIntegratedPipeline)
				throw new NotSupportedException("The mixed-authentication-module requires integrated pipeline-mode.");

			this.ValidateConfiguration(context);

			this.FormsAuthenticationModule.Enabled = true;

			this.FormsAuthentication.Initialize();

			context.AuthenticateRequest += this.OnAuthenticateRequest;
			context.EndRequest += this.OnEndRequest;
		}

		protected internal virtual void ValidateConfiguration(HttpApplication httpApplication)
		{
			if(httpApplication == null)
				throw new ArgumentNullException("httpApplication");

			if(this.AuthenticationSection.Mode != AuthenticationMode.Forms)
				throw new NotSupportedException("The mixed-authentication-module requires authentication-mode \"Forms\".");

			foreach(var name in httpApplication.Modules.Cast<string>())
			{
				var type = httpApplication.Modules.Get(name).GetType();

				if(this.DisallowedModuleTypes.Contains(type))
					throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "The mixed-authentication-module requires that the module \"{0}\" of type \"{1}\" is removed.", name, type));
			}
		}

		#endregion

		#region Eventhandlers

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