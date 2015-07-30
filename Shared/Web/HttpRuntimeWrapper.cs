using System.Web;

namespace Shared.Web
{
	public class HttpRuntimeWrapper : IHttpRuntime
	{
		#region Properties

		public virtual bool UsingIntegratedPipeline
		{
			get { return HttpRuntime.UsingIntegratedPipeline; }
		}

		#endregion
	}
}