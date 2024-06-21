using PassionProject2024.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject2024.Controllers
{
        public class BuildController : Controller
        {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static BuildController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44324/api/builddata/");
        }

        // GET: Build/List
        public ActionResult List()
        {
            string url = "listbuilds";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<BuildDto> builds = response.Content.ReadAsAsync<IEnumerable<BuildDto>>().Result;
            return View(builds);
        }

        public ActionResult Details(int id)
        {
            string url = $"findbuild/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            BuildDto build = response.Content.ReadAsAsync<BuildDto>().Result;

            // Get the list of components for the dropdowns
            string componentUrl = "http://localhost:44324/api/componentdata/listcomponents";
            HttpResponseMessage componentResponse = client.GetAsync(componentUrl).Result;
            IEnumerable<ComponentDto> components = componentResponse.Content.ReadAsAsync<IEnumerable<ComponentDto>>().Result;

            // Filter components by type
            ViewBag.CPUs = components.Where(c => c.Type == "CPU").ToList();
            ViewBag.GPUs = components.Where(c => c.Type == "GPU").ToList();
            ViewBag.RAMs = components.Where(c => c.Type == "RAM").ToList();
            ViewBag.SSDs = components.Where(c => c.Type == "SSD").ToList();
            ViewBag.PSUs = components.Where(c => c.Type == "PSU").ToList();

            return View(build);
        }


        public ActionResult New()
        {
            // Get the list of components for the dropdowns
            string url = "http://localhost:44324/api/componentdata/listcomponents";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ComponentDto> components = response.Content.ReadAsAsync<IEnumerable<ComponentDto>>().Result;

            // Filter components by type
            ViewBag.CPUs = components.Where(c => c.Type == "CPU").ToList();
            ViewBag.GPUs = components.Where(c => c.Type == "GPU").ToList();
            ViewBag.RAMs = components.Where(c => c.Type == "RAM").ToList();
            ViewBag.SSDs = components.Where(c => c.Type == "SSD").ToList();
            ViewBag.PSUs = components.Where(c => c.Type == "PSU").ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(BuildDto buildDto, List<int> componentIds)
        {
            if (ModelState.IsValid)
            {
                buildDto.Components = componentIds.Select(id => new ComponentDto { ComponentID = id }).ToList();

                string url = "addbuild";
                string jsonPayload = jss.Serialize(buildDto);
                HttpContent content = new StringContent(jsonPayload);
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    var errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    System.Diagnostics.Debug.WriteLine(errorMessage);
                    ViewBag.ErrorMessage = errorMessage;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent}");
                    ViewBag.ResponseContent = responseContent;
                    return View("Error");
                }
            }

            string componentUrl = "http://localhost:44324/api/componentdata/listcomponents";
            HttpResponseMessage componentResponse = client.GetAsync(componentUrl).Result;
            IEnumerable<ComponentDto> components = componentResponse.Content.ReadAsAsync<IEnumerable<ComponentDto>>().Result;

            ViewBag.CPUs = components.Where(c => c.Type == "CPU").ToList();
            ViewBag.GPUs = components.Where(c => c.Type == "GPU").ToList();
            ViewBag.RAMs = components.Where(c => c.Type == "RAM").ToList();
            ViewBag.SSDs = components.Where(c => c.Type == "SSD").ToList();
            ViewBag.PSUs = components.Where(c => c.Type == "PSU").ToList();

            return View("New", buildDto);
        }

        // GET: Build/Edit/5
        public ActionResult Edit(int id)
        {
            string url = $"findbuild/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                BuildDto build = response.Content.ReadAsAsync<BuildDto>().Result;
                return Json(build, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }


        // POST: Build/Update/5
        [HttpPost]
        public ActionResult Update(int id, BuildDto buildDto, List<int> componentIds)
        {
            if (ModelState.IsValid)
            {
                buildDto.Components = componentIds.Select(componentId => new ComponentDto { ComponentID = componentId }).ToList();

                string url = $"updatebuild/{id}";
                string jsonPayload = jss.Serialize(buildDto);
                HttpContent content = new StringContent(jsonPayload);
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    var errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    System.Diagnostics.Debug.WriteLine(errorMessage);
                    return Json(new { success = false, message = errorMessage });
                }
            }

            // If we reach here, there was a problem with the model state
            return Json(new { success = false, message = "Invalid model state" });
        }

        // POST: Build/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = $"deletebuild/{id}";
            HttpResponseMessage response = client.PostAsync(url, null).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                var errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                System.Diagnostics.Debug.WriteLine(errorMessage);
                return Json(new { success = false });
            }
        }


    }
}