using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using Shared.Configuration;
using Shared.Web;
using Shared.Web.Security;
using StructureMap.Configuration.DSL;
using StructureMap.Web;

namespace Shared.StructureMap
{
	public class Registry : global::StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		public Registry()
		{
			Register(this);
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		public static void Register(IProfileRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException("registry");

			registry.For<AuthenticationSection>().Singleton().Use(() => (AuthenticationSection) ConfigurationManager.GetSection("system.web/authentication"));
			registry.For<IConfigurationManager>().Singleton().Use<ConfigurationManagerWrapper>();
			registry.For<IFormsAuthentication>().Singleton().Use<FormsAuthenticationWrapper>();
			registry.For<IFormsAuthenticationModule>().Singleton().Use<FormsAuthenticationModuleWrapper>();
			registry.For<IHttpRuntime>().Singleton().Use<HttpRuntimeWrapper>();

			// Web
			registry.For<HttpApplicationState>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Application);
			registry.For<HttpApplicationStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpApplicationStateWrapper>();
			registry.For<HttpContext>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current);
			registry.For<HttpContextBase>().HybridHttpOrThreadLocalScoped().Use<HttpContextWrapper>();
			registry.For<HttpRequest>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Request);
			registry.For<HttpRequestBase>().HybridHttpOrThreadLocalScoped().Use<HttpRequestWrapper>();
			registry.For<HttpResponse>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Response);
			registry.For<HttpResponseBase>().HybridHttpOrThreadLocalScoped().Use<HttpResponseWrapper>();
			registry.For<HttpServerUtility>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Server);
			registry.For<HttpServerUtilityBase>().HybridHttpOrThreadLocalScoped().Use<HttpServerUtilityWrapper>();
			registry.For<HttpSessionState>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Session);
			registry.For<HttpSessionStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpSessionStateWrapper>();
		}

		#endregion
	}
}