﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Shared.StructureMap;
using StructureMap;
using ServiceLocator = Shared.InversionOfControl.ServiceLocator;
using StructureMapDependencyResolver = Shared.StructureMap.Web.Mvc.DependencyResolver;
using StructureMapServiceLocator = Shared.StructureMap.ServiceLocator;

namespace MvcApplication
{
	public class Global : HttpApplication
	{
		#region Constructors

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public Global()
		{
			var container = new Container(new Registry());

			ServiceLocator.Instance = new StructureMapServiceLocator(container);

			DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
		}

		#endregion

		#region Methods

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {}
		protected void Application_BeginRequest(object sender, EventArgs e) {}
		protected void Application_End(object sender, EventArgs e) {}
		protected void Application_Error(object sender, EventArgs e) {}

		protected void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			RouteTable.Routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);
		}

		protected void Session_End(object sender, EventArgs e) {}
		protected void Session_Start(object sender, EventArgs e) {}

		#endregion
	}
}