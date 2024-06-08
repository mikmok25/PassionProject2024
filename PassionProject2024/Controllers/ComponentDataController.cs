using PassionProject2024.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PassionProject2024.Controllers
{
    public class ComponentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ComponentData/ListComponents
        [HttpGet]
        public IEnumerable<ComponentDto> ListComponents()
        {
            List<Component> Components = db.Components.ToList();
            List<ComponentDto> ComponentDtos = new List<ComponentDto>();

            Components.ForEach(c => ComponentDtos.Add(new ComponentDto()
            {
                ComponentID = c.ComponentID,
                Name = c.Name,
                Type = c.Type,
                Manufacturer = c.Manufacturer,
                Price = c.Price,
                ImagePath = c.ImagePath
            }));

            return ComponentDtos;
        }

        // GET: api/ComponentData/FindComponent/5
        [ResponseType(typeof(ComponentDto))]
        [HttpGet]
        [Route("api/componentdata/findcomponent/{id}")]
        public IHttpActionResult FindComponent(int id)
        {
            Component Component = db.Components.Find(id);
            if (Component == null)
            {
                return NotFound();
            }

            ComponentDto ComponentDto = new ComponentDto()
            {
                ComponentID = Component.ComponentID,
                Name = Component.Name,
                Type = Component.Type,
                Manufacturer = Component.Manufacturer,
                Price = Component.Price,
                ImagePath = Component.ImagePath
            };

            return Ok(ComponentDto);
        }

        // POST: api/ComponentData/AddComponent
        [ResponseType(typeof(Component))]
        [HttpPost]
        public IHttpActionResult AddComponent(Component component)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Components.Add(component);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/ComponentData/UpdateComponent/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/componentdata/updatecomponent/{id}")]
        public IHttpActionResult UpdateComponent(int id, Component component)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != component.ComponentID)
            {
                return BadRequest();
            }

            db.Entry(component).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/ComponentData/DeleteComponent/5
        [ResponseType(typeof(Component))]
        [HttpPost]
        [Route("api/componentdata/deletecomponent/{id}")]
        public IHttpActionResult DeleteComponent(int id)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return NotFound();
            }

            db.Components.Remove(component);
            db.SaveChanges();

            return Ok(component);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComponentExists(int id)
        {
            return db.Components.Count(e => e.ComponentID == id) > 0;
        }
    }
}

