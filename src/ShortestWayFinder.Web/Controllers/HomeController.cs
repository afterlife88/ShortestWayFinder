using Microsoft.AspNetCore.Mvc;


namespace ShortestWayFinder.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("index");
        }
    }
}
