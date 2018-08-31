using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers.AdditionalControllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Additional Controller - Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult About()
        {
            ViewBag.Controller = "Additional Controller - Home";
            ViewBag.Action = "About";
            return View("ActionName");
        }
    }
}