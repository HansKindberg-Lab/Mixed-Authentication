using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Shared.StructureMap;
using StructureMap;
using ServiceLocator = Shared.InversionOfControl.ServiceLocator;
using StructureMapServiceLocator = Shared.StructureMap.ServiceLocator;

namespace WebFormsApplication
{
	public class Global : HttpApplication
	{
		#region Constructors

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public Global()
		{
			var container = new Container(new Registry());

			ServiceLocator.Instance = new StructureMapServiceLocator(container);
		}

		#endregion

		#region Methods

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {}
		protected void Application_BeginRequest(object sender, EventArgs e) {}
		protected void Application_End(object sender, EventArgs e) {}
		protected void Application_Error(object sender, EventArgs e) {}
		protected void Application_Start(object sender, EventArgs e) {}
		protected void Session_End(object sender, EventArgs e) {}
		protected void Session_Start(object sender, EventArgs e) {}

		#endregion
	}
}