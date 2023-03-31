using agricultural_guide.Models;
using agricultural_guide.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class SalesLocationController : Controller
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
            List<crop> dataList = new List<crop>();
            List<governorate> governorates = new List<governorate>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "crop"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<crop>>(sections.Result);
                } 
                using (var response = httpClient.GetAsync(uri.url + "governorate"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    governorates = JsonConvert.DeserializeObject<List<governorate>>(sections.Result);
                }
                
            }
            var location = new Location();
            location.crops = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
            location.governorates = governorates.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });
            return View(location);
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
                multiContent.Add(new StringContent(home.locationX), "locationX");
                multiContent.Add(new StringContent(home.crop_id), "crop_id");
                multiContent.Add(new StringContent(home.governorate_id), "governorate_id");
                multiContent.Add(new StringContent(home.locationY), "locationY");
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
                multiContent.Add(new StringContent(home.locationX), "locationX");
                multiContent.Add(new StringContent(home.locationY), "locationY");
                multiContent.Add(new StringContent(home.crop_id), "crop_id");
                multiContent.Add(new StringContent(home.governorate_id), "governorate_id");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Location));
        }


        public IActionResult cities()
        {

            List<Cities> dataList = new List<Cities>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "cities"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Cities>>(sections.Result);
                }
            }
            return View(dataList);
        }







        public ActionResult getcities(JqueryDatatableParam param)
        {

            List<Cities> datatablemodels = new List<Cities>();
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10, 20, 50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;

            // getting all Customer data  

            //Sorting  



            IEnumerable<Cities> filteredCompanies;

            List<string> tests = new List<string>();
            List<Cities> dataList = new List<Cities>();
            string type;
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "cities"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Cities>>(sections.Result);
                }
            }
            foreach (var item in dataList)
            {

                datatablemodels.Add(item);

            }


            if (!string.IsNullOrEmpty(searchValue))
            {
                filteredCompanies = datatablemodels
                         .Where(c => c.name.Contains(searchValue)
                                     ||
                          c.description.Contains(searchValue));
            }
            else
            {
                filteredCompanies = datatablemodels;
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                filteredCompanies = filteredCompanies.OrderBy(c => sortColumn);
            }

            var displayedCompanies = filteredCompanies;

            //This method is returning the IEnumerable employee from database
            var result = from c in displayedCompanies
                         select new[] { c.name, c.description, c._id };



            var data = result.Skip(skip).Take(pageSize).ToList();
            var totalRecords = displayedCompanies.Count();



            return Json(new
            {
                draw = draw,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                data = data
            });
        }








        public ActionResult add_cities()
        {

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_cities(Cities home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "cities");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(cities));
        }

        public ActionResult Deletecities(string id)
        {
            try
            {
                Location? home = new Location();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}cities/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<Location>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(cities));
            }
            catch
            {
                return View("cities");
            }
        }



        [HttpGet]

        public ActionResult Editcities(string id)
        {
            Cities? home = new Cities();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}cities/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    home = JsonConvert.DeserializeObject<Cities>(sections.Result);
                }

            }

            return View(home);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editcities(string id, Location home)
        {
            home._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "cities/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(Location));
        }




        public IActionResult governorate()
        {

            List<governorate> dataList = new List<governorate>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "governorate"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<governorate>>(sections.Result);
                }
            }
            return View(dataList);
        }



        public ActionResult add_governorate()
        {


            List<Cities> dataList = new List<Cities>();


            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "cities"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<Cities>>(sections.Result);
                }
            }

            var model = new governorate();
            model.cities = dataList.ToList().Select(x => new SelectListItem
            {
                Value = x._id,
                Text = x.name
            });

            return View(model);

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_governorate(governorate home)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "governorate");

            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }


            return RedirectToAction(nameof(governorate));
        }

        public ActionResult Deletegovernorate(string id)
        {
            try
            {
                governorate? home = new governorate();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}governorate/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<governorate>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(governorate));
            }
            catch
            {
                return View("governorate");
            }
        }



        [HttpGet]

        public ActionResult Editgovernorate(string id)
        {
            governorate? home = new governorate();
            List<Cities> city = new List<Cities>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}governorate/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    home = JsonConvert.DeserializeObject<governorate>(sections.Result);
                }
                using (var response = httpClient.GetAsync($"{uri.url}cities"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    city = JsonConvert.DeserializeObject<List<Cities>>(sections.Result);
                }
                home.cities = city.ToList().Select(x => new SelectListItem
                {
                    Value = x._id,
                    Text = x.name
                });



            }

            return View(home);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editgovernorate(string id, governorate home)
        {
            home._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "governorate/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");

                using (var response = httpClient.PutAsync(full_path, content))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<Location>(apiResponse.Result);
                }
            }
            return RedirectToAction(nameof(governorate));
        }

























        public IActionResult sales_adviser(string id = "y")
        {
            var vm = new salesvm();
            var saels = new List<sales_adviser>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "saleadviser_by_crop/" + id))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    vm.sales = JsonConvert.DeserializeObject<List<sales_adviser>>(sections.Result);
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
            List<string> location = new List<string>();

            foreach (var ir in vm.sales)
            {
                location.Add(ir.loaction_id);

            }
            var query =
            (from stages in location
             join fers in vm.sales on stages equals fers.loaction_id into gj
             group gj by stages);


            vm.sales_by_loction = query;

            return View(vm);
        }






    }
}
