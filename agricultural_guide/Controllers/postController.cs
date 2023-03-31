using Microsoft.AspNetCore.Mvc;
using agricultural_guide.Models;
using Newtonsoft.Json;

namespace agricultural_guide.Controllers
{
    public class postController : Controller
    {

        URI uri = new URI();


        public IActionResult post()
        {
            List<string> tests = new List<string>();
            List<posts> dataList = new List<posts>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "post"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<posts>>(sections.Result);
                }
            }

            return View(dataList);
        }





    
      















    }
}
