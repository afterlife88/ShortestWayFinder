using Microsoft.AspNetCore.Mvc;


namespace ShortestWayFinder.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            // Just returning index.html to use angular on the client
            return View("index");
        }
    }
}
