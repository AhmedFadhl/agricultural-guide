using Microsoft.AspNetCore.Mvc;
using agricultural_guide.Models;
using Newtonsoft.Json;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class SalesController : Controller
    {
        URI uri = new URI();
        public IActionResult Location()
        {

            List<Location> dataList = new List<Location>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "salelocation"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Location>>(sections.Result);
                }
            }
            return View(dataList);
        }



        public ActionResult add_Location()
        {

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_Location(Location home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "salelocation");

            using (var httpClient = new HttpClient())
            {
                byte[] data;
                using (var br = new BinaryReader(home.saleLocation_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.saleLocation_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);



                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(bytes, "saleLocation_image", home.saleLocation_image.FileName);
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.location), "location");
                multiContent.Add(new StringContent(home.map_link), "map_link");
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(Location));
        }

        public ActionResult Deletelocation(string id)
        {
            try
            {
                Location? home = new Location();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}salelocation/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<Location>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(Location));
            }
            catch
            {
                return View("Location");
            }
        }



        [HttpGet]

        public ActionResult Editlocation(string id)
        {
            Location? home = new Location();
            List<crop_type> dataList = new List<crop_type>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}salelocation/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    home = JsonConvert.DeserializeObject<Location>(sections.Result);
                }

            }

            return View(home);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editlocation(string id, Location home)
        {
            home._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "salelocation/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                if (home.saleLocation_image != null)
                {

                    using (var br = new BinaryReader(home.saleLocation_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)home.saleLocation_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "saleLocation_image", home.saleLocation_image.FileName);
                }
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.location), "location");
                multiContent.Add(new StringContent(home.map_link), "map_link");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Location));
        }







    }
}
