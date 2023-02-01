using Microsoft.AspNetCore.Mvc;
using agricultural_guide.Models;
using Newtonsoft.Json;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class LoginController : Controller
    {
    private readonly IHttpContextAccessor context;

        URI uri = new URI();


        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }
        public IActionResult Login()
        {
            context.HttpContext.Session.SetString("user","");
            context.HttpContext.Session.SetString("phone","");
            return View();
        }  
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(users users)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
                    using (var response = httpClient.PostAsync(uri.url + "user", content))
                    {
                        var apiResponse = response.Result.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<users>(apiResponse.Result);
                    }
                }
                context.HttpContext.Session.SetString("user", users.name);
                context.HttpContext.Session.SetString("phone", users.phone);
                if (users.image_path != null)
                {
                context.HttpContext.Session.SetString("image", users.image_path);
                }
                if (users.usertype != null)
                {
                context.HttpContext.Session.SetInt32("type", users.usertype);
                }

                return RedirectToAction("Index", "Home");

            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Login));
            }
        }






    }
}
