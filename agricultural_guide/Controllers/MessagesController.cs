using Microsoft.AspNetCore.Mvc;

namespace agricultural_guide.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Messages()
        {
            return View();
        }
    }
}
