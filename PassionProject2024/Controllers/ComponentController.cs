using PassionProject2024.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject2024.Controllers
{
    public class ComponentController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ComponentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44324/api/componentdata/");
        }

        public ActionResult List()
        {
            string url = "listcomponents";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ComponentDto> components = response.Content.ReadAsAsync<IEnumerable<ComponentDto>>().Result;
            return View(components);
        }

        // GET: Component/Details/5
        public ActionResult Details(int id)
        {
            string url = $"findcomponent/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                ComponentDto component = response.Content.ReadAsAsync<ComponentDto>().Result;
                return View(component);
            }
            return HttpNotFound();
        }

        // GET: Component/Edit/5
        public ActionResult Edit(int id)
        {
            string url = $"findcomponent/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                ComponentDto component = response.Content.ReadAsAsync<ComponentDto>().Result;
                return Json(component, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        // POST: Component/Create
        [HttpPost]
        public ActionResult Create(Component component, HttpPostedFileBase ImagePath, string ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null && ImagePath.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImagePath.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    ImagePath.SaveAs(path);
                    component.ImagePath = "~/Images/" + fileName;
                }
                else if (!string.IsNullOrEmpty(ImageUrl))
                {
                    component.ImagePath = ImageUrl;
                }

                string url = "addcomponent";
                string jsonPayload = jss.Serialize(component);
                HttpContent content = new StringContent(jsonPayload);
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }

            return Json(new { success = false });
        }

        // POST: Component/Update/5
        [HttpPost]
        public ActionResult Update(int id, Component component, HttpPostedFileBase ImagePath, string ImageUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string existingImagePath = null;

                    // Fetch the existing component to get the current image path
                    string url = $"findcomponent/{id}";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ComponentDto existingComponent = response.Content.ReadAsAsync<ComponentDto>().Result;
                        existingImagePath = existingComponent.ImagePath;
                    }

                    if (ImagePath != null && ImagePath.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(ImagePath.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                        var directory = Path.GetDirectoryName(path);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        ImagePath.SaveAs(path);
                        component.ImagePath = "~/Images/" + fileName;
                    }
                    else if (!string.IsNullOrEmpty(ImageUrl))
                    {
                        component.ImagePath = ImageUrl;
                    }
                    else
                    {
                        // Retain the existing image path if no new image is provided
                        component.ImagePath = existingImagePath;
                    }

                    url = $"updatecomponent/{id}";
                    string jsonPayload = jss.Serialize(component);
                    HttpContent content = new StringContent(jsonPayload);
                    content.Headers.ContentType.MediaType = "application/json";
                    response = client.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }

            return Json(new { success = false });
        }

        // GET: Component/Delete/5
        public ActionResult Delete(int id)
        {
            string url = $"findcomponent/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                ComponentDto component = response.Content.ReadAsAsync<ComponentDto>().Result;
                return Json(component, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        // POST: Component/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string url = $"deletecomponent/{id}";
            HttpResponseMessage response = client.PostAsync(url, null).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        public ActionResult Error()
        {
            return View();
        }



    }
}