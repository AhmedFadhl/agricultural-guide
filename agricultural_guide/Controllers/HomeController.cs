using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace agricultural_guide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var phone = HttpContext.Session.GetString("phone");
            var name = HttpContext.Session.GetString("user");

            if(phone!=null&&name!=null&&(phone!=""&&name!=""))
            {
            return View();
            }
            else
            {
            return View();
               // return RedirectToAction("Login", "Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}