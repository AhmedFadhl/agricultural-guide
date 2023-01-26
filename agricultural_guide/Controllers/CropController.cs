
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
            List<string> tests = new List<string>();
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
        public  ActionResult addcrop(crop home)
        {
            string file_name = string.Empty;
                string full_path = Path.Combine(uri.url, "crop");

            using (var httpClient = new HttpClient())
            {
                byte[] data;
                using (var br = new BinaryReader(home.crop_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.crop_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);



                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(bytes, "crop_image", home.crop_image.FileName);
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.description), "description");
                multiContent.Add(new StringContent(home.crop_type), "crop_type");
                multiContent.Add(new StringContent(home.type.ToString()), "type");
                multiContent.Add(new StringContent(home.state.ToString()), "state");
                using (var response = httpClient.PostAsync(full_path, multiContent))
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
           
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "crop/",id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(crop), Encoding.UTF8, "application/json");

                if (crop.crop_image != null)
                {

                    using (var br = new BinaryReader(crop.crop_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)crop.crop_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "crop_image", crop.crop_image.FileName);
                }
                multiContent.Add(new StringContent(crop.name), "name");
                multiContent.Add(new StringContent(crop.description), "description");
                multiContent.Add(new StringContent(crop.crop_type), "crop_type");
                multiContent.Add(new StringContent(crop.type.ToString()), "type");
                multiContent.Add(new StringContent(crop.state.ToString()), "state");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(crop));
        }


        public ActionResult Acceptcrops(string id)
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
            }
            crop.state = 2;

            return Acceptcrop(id ,crop);
        }




        [ValidateAntiForgeryToken]
        public ActionResult Acceptcrop(string id, crop crop)
        {
            crop._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "crop/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(crop), Encoding.UTF8, "application/json");

                if (crop.crop_image != null)
                {

                    using (var br = new BinaryReader(crop.crop_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)crop.crop_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "crop_image", crop.crop_image.FileName);
                }
                multiContent.Add(new StringContent(crop.name), "name");
                multiContent.Add(new StringContent(crop.description), "description");
                multiContent.Add(new StringContent(crop.crop_type), "crop_type");
                multiContent.Add(new StringContent(crop.type.ToString()), "type");
                multiContent.Add(new StringContent(crop.state.ToString()), "state");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(crop));
        }



        public IActionResult crop_type()
        {
            List<crop_type> dataList = new List<crop_type>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "type_crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<crop_type>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult add_crop_type()
        {

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_crop_type(crop_type crop_Type)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(crop_Type), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "type_crop", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop_type>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(crop_type));
        }


        public ActionResult Delete_crop_type(string id)
        {
            try
            {
                crop_type? home = new crop_type();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}type_crop/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<crop_type>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(crop_type));
            }
            catch
            {
                return View("crop_type");
            }
        }


        [HttpGet]

        public ActionResult Edit_crop_type(string id)
        {
            crop_type? crop_type = new crop_type();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}type_crop/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    crop_type = JsonConvert.DeserializeObject<crop_type>(sections.Result);
                }
            }
            return View(crop_type);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_crop_type(string id, crop_type crop)
        {
            crop._id = id;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(crop), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync($"{uri.url}type_crop/{id}", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<crop_type>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(crop_type));
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
