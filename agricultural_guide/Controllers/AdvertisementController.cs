using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class AdvertisementController : Controller
    {
        URI uri = new URI();
        public IActionResult advertiser()
        {

            List<Advertisers> dataList = new List<Advertisers>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "advertisers"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Advertisers>>(sections.Result);
                }
            }

            return View(dataList);
        }







        public ActionResult addadvertiser()
        {


            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addadvertiser(Advertisers home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "advertisers");

            using (var httpClient = new HttpClient())
            {
               
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.description), "description");
                multiContent.Add(new StringContent(home.email), "email");
                multiContent.Add(new StringContent(home.phone.ToString()), "phone");
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(advertiser));
        }
        public ActionResult Deleteadvertiser(string id)
        {
            try
            {
                Advertisers? home = new Advertisers();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}advertisers/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<Advertisers>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(advertiser));
            }
            catch
            {
                return View("advertiser");
            }
        }


        [HttpGet]

        public ActionResult Editadvertiser(string id)
        {
            Advertisers? advertiser = new Advertisers();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}advertisers/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    advertiser = JsonConvert.DeserializeObject<Advertisers>(sections.Result);
                }


            }


            return View(advertiser);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editadvertiser(string id, Advertisers advertiser)
        {
            advertiser._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "advertisers/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(advertiser), Encoding.UTF8, "application/json");

             
                multiContent.Add(new StringContent(advertiser.name), "name");
                multiContent.Add(new StringContent(advertiser.description), "description");
                multiContent.Add(new StringContent(advertiser.phone), "phone");
                multiContent.Add(new StringContent(advertiser.email.ToString()), "email");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Advertisers>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(advertiser));
        }


        public IActionResult advertisement()
        {
            List<Advertisments> dataList = new List<Advertisments>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "ads"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Advertisments>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult add_ads()
        {

            List<Advertisers> dataList = new List<Advertisers>();
            Advertisments crop = new Advertisments();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "advertisers"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Advertisers>>(sections.Result);
                }
            }

            var model = new Advertisments();
            model.advertisers = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });

            return View(model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_ads(Advertisments home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "ads");

            using (var httpClient = new HttpClient())
            {
                byte[] data;
                using (var br = new BinaryReader(home.ads_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.ads_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(bytes, "ads_image", home.ads_image.FileName);
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.description), "description");
                multiContent.Add(new StringContent(home.link), "link");
                multiContent.Add(new StringContent(home.id_advertiser.ToString()), "id_advertiser");
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(advertisement));




        }


        public ActionResult Delete_ads(string id)
        {
            try
            {
                Advertisments? home = new Advertisments();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}ads/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<Advertisments>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(advertisement));
            }
            catch
            {
                return View("advertisement");
            }
        }


        [HttpGet]

        public ActionResult Edit_ads(string id)
        {
            Advertisments? advertisement = new Advertisments();
            var dataList = new List<Advertisers>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}ads/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    advertisement = JsonConvert.DeserializeObject<Advertisments>(sections.Result);
                }

                using (var response = httpClient.GetAsync(uri.url + "advertisers"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Advertisers>>(sections.Result);
                }
            }
            advertisement.advertisers = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });



            return View(advertisement);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_ads(string id, Advertisments home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "ads/"+id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;

                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                if (home.ads_image != null)
                {

                using (var br = new BinaryReader(home.ads_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.ads_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);



                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(bytes, "ads_image", home.ads_image.FileName);
                }
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.description), "description");
                multiContent.Add(new StringContent(home.link), "link");
                multiContent.Add(new StringContent(home.id_advertiser.ToString()), "id_advertiser");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Advertisments>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(advertisement));
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





    }
}
