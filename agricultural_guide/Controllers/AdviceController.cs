using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class AdviceController : Controller
    {
        URI uri = new URI();

        public IActionResult advice()
        {

            List<advice> dataList = new List<advice>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "advice"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<advice>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult addadvice()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addadvice(advice home)
        {
            var list = home.important[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var lists = home.How[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();


            home.important = list;
            home.How = lists;
            string full_path = Path.Combine(uri.url, "advice");

            using (var httpClient = new HttpClient())
            {
                byte[] data;
                using (var br = new BinaryReader(home.advice_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.advice_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(bytes, "advice_image", home.advice_image.FileName);
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.description), "description");

                for (int i = 0; i < list.Length; i++)
                {
                    multiContent.Add(new StringContent(home.important[i]), "important");
                }

                for (int i = 0; i < lists.Length; i++)
                {
                    multiContent.Add(new StringContent(home.How[i]), "how");
                }

                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(advice));
        }


        public ActionResult Deleteadvice(string id)
        {
            try
            {
                advice? home = new advice();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}advice/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<advice>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(advice));
            }
            catch
            {
                return View("advice");
            }
        }


        [HttpGet]

        public ActionResult Editadvice(string id)
        {
            advice? advice = new advice();
            List<disease_type> dataList = new List<disease_type>();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}advice/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    advice = JsonConvert.DeserializeObject<advice>(sections.Result);
                }

            }



            return View(advice);
        }

        // POST: StudentController/Edit/5


        [ValidateAntiForgeryToken]
        public ActionResult Editadvice(string id, advice home)
        {
            var list = home.important[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var lists = home.How[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();


            home.important = list;
            home.How = lists;
            string full_path = Path.Combine(uri.url, "advice",id);

            using (var httpClient = new HttpClient())
            {
                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                if (home.advice_image != null)
                {

                byte[] data;
                using (var br = new BinaryReader(home.advice_image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)home.advice_image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                multiContent.Add(bytes, "advice_image", home.advice_image.FileName);
                }


                StringContent content = new StringContent(JsonConvert.SerializeObject(home), Encoding.UTF8, "application/json");
                multiContent.Add(new StringContent(home.name), "name");
                multiContent.Add(new StringContent(home.description), "description");

                for (int i = 0; i < list.Length; i++)
                {
                    multiContent.Add(new StringContent(home.important[i]), "important");
                }

                for (int i = 0; i < lists.Length; i++)
                {
                    multiContent.Add(new StringContent(home.How[i]), "how");
                }

                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<disease>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(advice));
        }








    }
}
