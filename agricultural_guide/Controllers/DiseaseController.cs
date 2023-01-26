using agricultural_guide.Models;
using agricultural_guide.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class DiseaseController : Controller
    {
        URI uri = new URI();

        public IActionResult disease()
        {

            List<disease> dataList = new List<disease>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "disease"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<disease>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult adddisease()
        {

            List<disease_type> dataList = new List<disease_type>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "disease_type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<disease_type>>(sections.Result);
                }
            }

            var model = new disease();
            model.disease_types = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });

            return View(model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult adddisease(disease home)
        {
            var list = home.prevention[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var lists = home.symptoms[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();


            home.prevention = list;
            home.symptoms = lists;
            string full_path = Path.Combine(uri.url, "disease");

            using (var httpClient = new HttpClient())
            {
                byte[] data;
                using (var br = new BinaryReader(home.disease_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.disease_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(bytes, "Disease_image", home.disease_image.FileName);
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.antiorganic), "antiorganic");
                multiContent.Add(new StringContent(home.chemicalcontrol), "chemicalcontrol");
                multiContent.Add(new StringContent(home.dis_type_id), "dis_type_id");

                for(int i=0; i < list.Length; i++)
                {
                multiContent.Add(new StringContent(list[i]), "prevention");
                }

                for (int i = 0; i < lists.Length; i++)
                {
                    multiContent.Add(new StringContent(lists[i]), "symptoms");
                }

                multiContent.Add(new StringContent(home.reason), "reason");
                multiContent.Add(new StringContent(home.recommendations), "recommendations");
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(disease));
        }
        public ActionResult DeleteDisease(string id)
        {
            try
            {
                disease? home = new disease();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}disease/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<disease>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(disease));
            }
            catch
            {
                return View("disease");
            }
        }


        [HttpGet]

        public ActionResult Editdisease(string id)
        {
            disease? disease = new disease();
            List<disease_type> dataList = new List<disease_type>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}disease/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    disease = JsonConvert.DeserializeObject<disease>(sections.Result);
                }

                using (var response = httpClient.GetAsync(uri.url + "disease_type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<disease_type>>(sections.Result);

                }

            }

            disease.disease_types = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });


            return View(disease);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editdisease(string id, disease disease)
        {
            disease._id = id;
            var list = disease.prevention[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var lists = disease.symptoms[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();


            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "disease/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(disease), Encoding.UTF8, "application/json");

                if (disease.disease_image != null)
                {

                    using (var br = new BinaryReader(disease.disease_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)disease.disease_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "disease_image", disease.disease_image.FileName);
                }
                multiContent.Add(new StringContent(disease.name), "name");
                multiContent.Add(new StringContent(disease.antiorganic), "anti_organic");
                multiContent.Add(new StringContent(disease.chemicalcontrol), "chemical_control");
                multiContent.Add(new StringContent(disease.dis_type_id), "dis_type_id");
                multiContent.Add(new StringContent(JsonConvert.SerializeObject(list)), "prevention");
                multiContent.Add(new StringContent(disease.reason), "reason");
                multiContent.Add(new StringContent(disease.recommendations), "recommendations");
                multiContent.Add(new StringContent(JsonConvert.SerializeObject(lists)), "symptoms");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(disease));
        }



        public IActionResult disease_type()
        {
            List<disease_type> dataList = new List<disease_type>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "disease_type"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<disease_type>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult add_disease_type()
        {

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_disease_type(disease_type disease_type)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(disease_type), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "disease_type", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease_type>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(disease_type));
        }


        public ActionResult Delete_disease_type(string id)
        {
            try
            {
                disease_type? home = new disease_type();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}disease_type/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<disease_type>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(disease_type));
            }
            catch
            {
                return View("disease_type");
            }
        }


        [HttpGet]

        public ActionResult Edit_disease_type(string id)
        {
            disease_type? disease_type = new disease_type();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}disease_type/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    disease_type = JsonConvert.DeserializeObject<disease_type>(sections.Result);
                }
            }
            return View(disease_type);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Edit_disease_type(string id, disease_type disease_type)
        {
            disease_type._id = id;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(disease_type), Encoding.UTF8, "application/json");
                using (var response = httpClient.PutAsync($"{uri.url}disease_type/{id}", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease_type>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(disease_type));
        }










        public IActionResult disease_stages(string id = "y" ,string name="noth")
        {
            var vm = new disease_vm();
            var stages = new List<disease_stages>();
            var dis_stage = new List<disease_stages>();



            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "stages_by_crop/" + id))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vm.clutrication_Stages = JsonConvert.DeserializeObject<List<disease_stages>>(sections.Result);
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vm.crops = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                }
            }


            vm.crop_id = id;
            List<string> stage = new List<string>();
            List<disease_stages> disease = new List<disease_stages>();
            disease = vm.clutrication_Stages;
            foreach (var ir in vm.clutrication_Stages)
            {
                stage.Add(ir.name);

            }
            if (name != "noth")
            {
            foreach(var item in vm.clutrication_Stages)
            {
                    if (item.name == name)
                    {
                        dis_stage.Add(item);
                    }
            }
                vm.clutrication_Stages = dis_stage;
            }

                var des = new List<disease>();
            foreach(var item in dis_stage)
            {
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "disease/"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                        des = JsonConvert.DeserializeObject<List<disease>>(sections.Result);
                    }
            }
              
            }


            vm.diseases = des;



            var query =
            (from stag in stage
             join fers in disease on stag equals fers.name 
             group fers by stag);


            vm.clutrication_Stage = query;

            return View(vm);
        }


       








        public ActionResult add_disease_stages()
        {

            List<crop> crop = new List<crop>();
            List<disease> diseases = new List<disease>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    crop = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                }
                using (var response = httpClient.GetAsync(uri.url + "disease"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    diseases = JsonConvert.DeserializeObject<List<disease>>(sections.Result);
                }
               
            }

            var model = new disease_stages();
            foreach (var item in crop)
            {
                if (item.state != 1)
                {
                    model.crops = crop.Select(x => new SelectListItem
                    {
                        Value = x._id,
                        Text = x.name
                    });
                }
            }
            model.diseases = diseases.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
          

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_disease_stages(disease_stages disease_Stage)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(disease_Stage), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(uri.url + "stages", content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease_stages>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(disease_stages));
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
