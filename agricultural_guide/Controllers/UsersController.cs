using agricultural_guide.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace agricultural_guide.Controllers
{
    public class UsersController : Controller
    {
        URI uri = new URI();

        public IActionResult advicer()
        {

            List<users> dataList = new List<users>();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync(uri.url + "user"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    dataList = JsonConvert.DeserializeObject<List<users>>(sections.Result);
                }
            }

            return View(dataList);
        }

        public ActionResult add_adviser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_adviser(users users)
        {
            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "user/");

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

                if (users.user_image != null)
                {

                    using (var br = new BinaryReader(users.user_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)users.user_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "user_image", users.user_image.FileName);
                }

                multiContent.Add(new StringContent(users.name), "name");
                multiContent.Add(new StringContent(users.phone), "phone");
                multiContent.Add(new StringContent(users.specialize), "specialize");
                multiContent.Add(new StringContent(users.about), "about");
                multiContent.Add(new StringContent(users.usertype.ToString()), "usertype");
                //  multiContent.Add(new StringContent(user.state.ToString()), "state");
                using (var response = httpClient.PostAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<users>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(users));
        }


        public ActionResult Delete_user(string id)
        {
            try
            {
                users? home = new users();

                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync($"{uri.url}user/{id}"))
                    {
                        var sections = response.Result.Content.ReadAsStringAsync();
                        home = JsonConvert.DeserializeObject<users>(sections.Result);
                    }
                }
                return RedirectToAction(nameof(users));
            }
            catch
            {
                return View("user");
            }
        }

        [HttpGet]

        public ActionResult Edit_user(string id)
        {
            users? user = new users();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}user/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<users>(sections.Result);
                }
            }
            return View(user);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit_user(string id, users user)
        {
            user._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "user/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                if (user.user_image != null)
                {

                    using (var br = new BinaryReader(user.user_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)user.user_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "user_image", user.user_image.FileName);
                }

                multiContent.Add(new StringContent(user.name), "name");
                multiContent.Add(new StringContent(user.phone), "phone");
                multiContent.Add(new StringContent(user.specialize), "specialize");
                multiContent.Add(new StringContent(user.usertype.ToString()), "usertype");
                multiContent.Add(new StringContent(user.about), "about");
                //  multiContent.Add(new StringContent(user.state.ToString()), "state");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<users>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(user));
        }

        public ActionResult Acceptaderiser(string id)
        {
            users? user = new users();

            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{uri.url}user/{id}"))
                {
                    var sections = response.Result.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<users>(sections.Result);
                }
            }
            user.state = 2;

            return Acceptcrop(id, user);
        }




        [ValidateAntiForgeryToken]
        public ActionResult Acceptcrop(string id, users user)
        {
            user._id = id;

            string file_name = string.Empty;
            string full_path = Path.Combine(uri.url, "user/", id);

            using (var httpClient = new HttpClient())
            {
                byte[] data;


                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                if (user.user_image != null)
                {

                    using (var br = new BinaryReader(user.user_image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)user.user_image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);

                    multiContent.Add(bytes, "user_image", user.user_image.FileName);
                }

                multiContent.Add(new StringContent(user.name), "name");
                multiContent.Add(new StringContent(user.phone), "phone");
                multiContent.Add(new StringContent(user.specialize), "specialize");
                multiContent.Add(new StringContent(user.usertype.ToString()), "usertype");
                //  multiContent.Add(new StringContent(user.state.ToString()), "state");
                multiContent.Add(new StringContent(user.about), "about");
                using (var response = httpClient.PutAsync(full_path, multiContent))
                {
                    var apiResponse = response.Result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<users>(apiResponse.Result);
                }
            }

            return RedirectToAction(nameof(users));
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
