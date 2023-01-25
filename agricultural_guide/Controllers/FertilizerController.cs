using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using agricultural_guide.ViewModels;

namespace agricultural_guide.Controllers
{
    public class FertilizerController : Controller
    {

        URI uri = new URI();

        public IActionResult fertilizer()
        {
            List<fertilizer> dataList = new List<fertilizer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "fertilizer"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<fertilizer>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult addfertilizer()
        {
           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addfertilizer(fertilizer fertilizer)
        {
            using (var httpClient = new HttpClient())
            {

                var content = new StringContent(JsonConvert.SerializeObject(fertilizer), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "fertilizer", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<fertilizer>(apiResponse.Result);
                }

                return RedirectToAction(nameof(fertilizer));

            }
        }
        public ActionResult Delete_fertilizer(string id)
        {
            try
            {
                fertilizer? fertilizer = new fertilizer();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}fertilizer/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        fertilizer = JsonConvert.DeserializeObject<fertilizer>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(fertilizer));
            }
            catch
            {
                return View("fertilizer");
            }
        }




        [HttpGet]

        public ActionResult Edit_fertilizer(string id)
        {
            fertilizer? fertilizer = new fertilizer();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}fertilizer/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    fertilizer = JsonConvert.DeserializeObject<fertilizer>(sections.Result);
                }
            }
            return View(fertilizer);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_fertilizer(string id, fertilizer fertilizer)
        {
            fertilizer._id = id;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(fertilizer), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync($"{uri.url}fertilizer/{id}", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<fertilizer>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(fertilizer));
        }






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

            List<fertilizer> dataList = new List<fertilizer>();
            crop crop = new crop();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "fertilizer"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<fertilizer>>(sections.Result);
                }
            }

            var model = new fer_type();
            model.fertilizers = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
            return View(model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult  addfer_type(fer_type home)
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


        public IActionResult fertilizer_stage()
        {
            List<fertilizer_stage> dataList = new List<fertilizer_stage>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "stages_fertilizer"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<fertilizer_stage>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult addfertilizer_stage()
        {

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addfertilizer_stage(fertilizer_stage home)
        {


            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "stages_fertilizer", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<fertilizer_stage>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(fertilizer_stage));
        }


        public ActionResult Deletefertilizer_stage(string id)
        {
            try
            {
                fertilizer_stage? fertilizer_stage = new fertilizer_stage();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}stages_fertilizer/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        fertilizer_stage = JsonConvert.DeserializeObject<fertilizer_stage>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(fertilizer_stage));
            }
            catch
            {
                return View("fertilizer_stage");
            }
        }


        [HttpGet]

        public ActionResult Editfertilizer_stage(string id)
        {
            fertilizer_stage? fertilizer_stage = new fertilizer_stage();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}stages_fertilizer/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    fertilizer_stage = JsonConvert.DeserializeObject<fertilizer_stage>(sections.Result);
                }
            }
            return View(fertilizer_stage);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editfertilizer_stage(string id, fertilizer_stage fertilizer_stage)
        {
            fertilizer_stage._id = id;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(fertilizer_stage), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync($"{uri.url}stages_fertilizer/{id}", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<fertilizer_stage>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(fertilizer_stage));
        }

        public IActionResult qty(string id="y")
        {
            var vm = new qtyvm();
            var qty = new List<qty>();
           


            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "QTY_by_crop/"+id))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vm.qty = JsonConvert.DeserializeObject<List<qty>>(sections.Result);
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vm.crop = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                }
            }


            vm.crop_id = id;
            List<string> fer = new List<string>();
            List<string> stage = new List<string>();

            foreach (var ir in vm.qty)
            {
                stage.Add(ir.stages_fertilizer_id);

            }
            var query =
            (from stages in stage
             join fers in vm.qty on stages equals fers.stages_fertilizer_id into gj
             group gj by stages);


            vm.fer = fer;
            vm.fertilizer_Stage = query;

            return View(vm);
        }



        public ActionResult add_qty()
        {

            List<crop> crop = new List<crop>();
            List<fertilizer> fertilizers = new List<fertilizer>();
            List<fertilizer_stage> fertilizer_Stages = new List<fertilizer_stage>();
            List<fer_type> fertilizer_type = new List<fer_type>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    crop = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "fertilizer"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    fertilizers = JsonConvert.DeserializeObject<List<fertilizer>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "stages_fertilizer"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    fertilizer_Stages = JsonConvert.DeserializeObject<List<fertilizer_stage>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "fertilizer_type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    fertilizer_type = JsonConvert.DeserializeObject<List<fer_type>>(sections.Result);
                }
            }

            var model = new qty();
            foreach (var item in crop)
            {
                if (item.state!=1)
            {
                model.crops = crop.Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
            }
            }
            model.fertilizers = fertilizers.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
            model.stages = fertilizer_Stages.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
            model.fer_types = fertilizer_type.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_qty(qty quantity)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(quantity), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "QTY", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<qty>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(qty));
        }


    }
}

