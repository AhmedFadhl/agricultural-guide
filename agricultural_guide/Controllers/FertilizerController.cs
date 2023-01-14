using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class FertilizerController : Controller
    {

        URI uri = new URI();
        public IActionResult fer_type()
        {
            List<fer_type> dataList = new List<fer_type>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "fertilizer_type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<fer_type>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult addfer_type()
        {


            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addfer_type(fer_type home)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "fertilizer_type", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<fer_type>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(fer_type));
        }


        public ActionResult Deletefer_type(string id)
        {
            try
            {
                fer_type? fer_type = new fer_type();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}fertilizer_type/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        fer_type = JsonConvert.DeserializeObject<fer_type>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(fer_type));
            }
            catch
            {
                return View("fer_type");
            }
        }


        [HttpGet]

        public ActionResult Editfer_type(string id)
        {
            fer_type? fer_type = new fer_type();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}fertilizer_type/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    fer_type = JsonConvert.DeserializeObject<fer_type>(sections.Result);
                }
            }
            return View(fer_type);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editfer_type(string id, fer_type fer_type)
        {
            fer_type._id = id;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(fer_type), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync($"{uri.url}fertilizer_type/{id}", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(fer_type));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
