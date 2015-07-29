using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class AdministrationController : Controller
	{
		#region Methods

		public virtual ActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}