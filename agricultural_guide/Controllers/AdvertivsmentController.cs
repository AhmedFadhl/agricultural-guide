using Microsoft.AspNetCore.Mvc;
using agricultural_guide.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace agricultural_guide.Controllers
{
    public class AdvertivsmentController : Controller
    {
       
            URI uri = new URI();

            public IActionResult Advertiser()
            {

                List<Advertisers> dataList = new List<Advertisers>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync(uri.url + "Advertisers"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        dataList = JsonConvert.DeserializeObject<List<Advertisers>>(sections.Result);
                    }
                }

                return View(dataList);
            }

            public ActionResult addAdvertiser()
            {
                return View();
            }





            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult addAdvertiser(Advertisers home)
            {
                string file_name = string.Empty;
                string full_path = Path.Combine(uri.url, "Advertisers");

                using (var httpClient = new HttpClient())
                {
                    byte[] data;
                    using (var br = new BinaryReader(home.advertiser_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)home.advertiser_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);



                    MultipartFormDataContent multiContent = new MultipartFormDataContent();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                    multiContent.Add(bytes, "advertiser_image", home.advertiser_image.FileName);
                    multiContent.Add(new StringContent(home.name), "name");
                    multiContent.Add(new StringContent(home.description), "description");
                    multiContent.Add(new StringContent(home.contact_link), "contact_link");
                    multiContent.Add(new StringContent(home.phone.ToString()), "phone");
                    using (var response = httpClient.PostAsync(full_path, multiContent))
                    {
                        var apiResponse = response.Result.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<Advertisers>(apiResponse.Result);
                    }
                }

                return RedirectToAction(nameof(Advertiser));
            }
            public ActionResult DeleteAdvertiser(string id)
            {
                try
                {
                    Advertisers? home = new Advertisers();

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = httpClient.DeleteAsync($"{uri.url}Advertiser/{id}"))
                        {
                            var sections = response.Result.Content.ReadAsStringAsync();
                            home = JsonConvert.DeserializeObject<Advertisers>(sections.Result);
                        }
                    }
                    return RedirectToAction(nameof(Advertiser));
                }
                catch
                {
                    return View("Advertiser");
                }
            }


            [HttpGet]

            public ActionResult EditAdvertiser(string id)
            {
                Advertisers Advertiser = new Advertisers();
                List<crop_type> dataList = new List<crop_type>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync($"{uri.url}Advertiser/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        Advertiser = JsonConvert.DeserializeObject<Advertisers>(sections.Result);
                    }
                }

                return View(Advertiser);
            }

            // POST: StudentController/Edit/5


            [ValidateAntiForgeryToken]
            public ActionResult EditAdvertiser(string id, Advertisers Advertiser)
            {
                Advertiser._id = id;

                string file_name = string.Empty;
                string full_path = Path.Combine(uri.url, "Advertiser/", id);

                using (var httpClient = new HttpClient())
                {
                    byte[] data;


                    MultipartFormDataContent multiContent = new MultipartFormDataContent();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Advertiser), Encoding.UTF8, "application/json");

                    if (Advertiser.advertiser_image != null)
                    {
                        using (var br = new BinaryReader(Advertiser.advertiser_image.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)Advertiser.advertiser_image.OpenReadStream().Length);
                        }
                        ByteArrayContent bytes = new ByteArrayContent(data);

                        multiContent.Add(bytes, "advertiser_image", Advertiser.advertiser_image.FileName);
                    }
                    multiContent.Add(new StringContent(Advertiser.name), "name");
                    multiContent.Add(new StringContent(Advertiser.description), "description");
                    multiContent.Add(new StringContent(Advertiser.contact_link), "contact_link");
                    multiContent.Add(new StringContent(Advertiser.phone.ToString()), "phone");
                    using (var response = httpClient.PutAsync(full_path, multiContent))
                    {
                        var apiResponse = response.Result.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<Advertisers>(apiResponse.Result);
                    }
                }

                return RedirectToAction(nameof(Advertiser));
            }


        public IActionResult Advertisment()
        {

            List<crop> dataList = new List<crop>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Advertisment"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult add_ads()
        {

            List<Advertisers> dataList = new List<Advertisers>();
           
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Advertiser"))
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
            string full_path = Path.Combine(uri.url, "Advertisment");

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
                multiContent.Add(new StringContent(home.ads), "ads");
                multiContent.Add(new StringContent(home.description), "description");
                multiContent.Add(new StringContent(home.advertiser), "advertiser");
                multiContent.Add(new StringContent(home.ad_link.ToString()), "ad_link");
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Advertisments>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Advertisment));
        }
        public ActionResult DeleteCrop(string id)
        {
            try
            {
                Advertisments? home = new Advertisments();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}Advertisment/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<Advertisments>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(crop));
            }
            catch
            {
                return View("Advertisment");
            }
        }


        [HttpGet]

        public ActionResult EditAdvertisment(string id)
        {
            Advertisments? Advertisments = new Advertisments();
            List<Advertisers> dataList = new List<Advertisers>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "Advertiser"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Advertisers>>(sections.Result);
                }
            }

            Advertisments.advertisers = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });


            return View(Advertisments);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult EditAdvertisment(string id, Advertisments Advertisment)
        {
            Advertisment._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "Advertisment/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(Advertisment), Encoding.UTF8, "application/json");

                if (Advertisment.ads_image != null)
                {

                    using (var br = new BinaryReader(Advertisment.ads_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)Advertisment.ads_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "ads_image", Advertisment.ads_image.FileName);
                }
                multiContent.Add(new StringContent(Advertisment.ads), "ads");
                multiContent.Add(new StringContent(Advertisment.description), "description");
                multiContent.Add(new StringContent(Advertisment.advertiser), "advertiser");
                multiContent.Add(new StringContent(Advertisment.ad_link.ToString()), "ad_link");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(Advertisment));
        }









    }


}
    
