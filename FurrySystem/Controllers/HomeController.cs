using System.Web.Mvc;

namespace FurrySystem.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Furry system";

			return View();
		}
	}
}