using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

		public virtual ActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}