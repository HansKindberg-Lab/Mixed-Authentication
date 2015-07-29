using System.Web.Mvc;

namespace MvcLabApplication.Controllers
{
	public class WindowsAuthenticationController : Controller
	{
		#region Methods

		public virtual ActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}