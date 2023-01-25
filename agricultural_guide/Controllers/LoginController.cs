
using Microsoft.AspNetCore.Mvc;

namespace agricultural_guide.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
    }
}
