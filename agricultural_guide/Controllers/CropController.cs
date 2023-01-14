using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;



using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;


namespace agricultural_guide.Controllers
{
    public class CropController : Controller
    {
        URI uri = new URI();






        public IActionResult crop()
        {
            List<crop> dataList = new List<crop>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                }
            }
            return View(dataList);
        }

        public ActionResult addcrop()
        {

            List<crop_type> dataList = new List<crop_type>();
            crop crop = new crop();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "type_crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<crop_type>>(sections.Result);
                }
            }

            var model = new crop();
            model.crop_types = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });

            return View(model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addcrop(crop home)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "crop", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(crop));
        }


        public ActionResult DeleteCrop(string id)
        {
            try
            {
                crop? home = new crop();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}crop/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<crop>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(crop));
            }
            catch
            {
                return View("crop");
            }
        }


        [HttpGet]

        public ActionResult Editcrop(string id)
        {
            crop? crop = new crop();
            List<crop_type> dataList = new List<crop_type>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}crop/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    crop = JsonConvert.DeserializeObject<crop>(sections.Result);
                }

                using (var response = httpClient.GetAsync(uri.url + "type_crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<crop_type>>(sections.Result);

                }

            }

            crop.crop_types = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });


            return View(crop);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editcrop(string id, crop crop)
        {
            crop._id = id;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(crop), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync($"{uri.url}crop/{id}", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(crop));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
