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

        /// <summary>
        /// Returns all components in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all components in the database.
        /// </returns>
        /// <example>
        /// GET: api/ComponentData/ListComponents
        /// </example>
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

        /// <summary>
        /// Returns a specific component by ID.
        /// </summary>
        /// <param name="id">The primary key of the component</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A component in the system matching the component ID
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// GET: api/ComponentData/FindComponent/5
        /// </example>
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

        /// <summary>
        /// Adds a new component to the system.
        /// </summary>
        /// <param name="component">JSON FORM DATA of a component</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Component ID, Component Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ComponentData/AddComponent
        /// FORM DATA: Component JSON Object
        /// </example>
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

        /// <summary>
        /// Updates a particular component in the system with POST Data input.
        /// </summary>
        /// <param name="id">Represents the Component ID primary key</param>
        /// <param name="component">JSON FORM DATA of a component</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ComponentData/UpdateComponent/5
        /// FORM DATA: Component JSON Object
        /// </example>
    
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


        /// <summary>
        /// Deletes a component from the system by its ID.
        /// </summary>
        /// <param name="id">The primary key of the component</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ComponentData/DeleteComponent/5
        /// FORM DATA: (empty)
        /// </example>
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

