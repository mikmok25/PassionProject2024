using PassionProject2024.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace PassionProject2024.Controllers
{
    public class BuildDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BuildData/ListBuilds
        [HttpGet]
        public IEnumerable<BuildDto> ListBuilds()
        {
            List<Build> builds = db.Builds.Include(b => b.BuildComponents.Select(bc => bc.Component)).ToList();
            List<BuildDto> buildDtos = new List<BuildDto>();

            builds.ForEach(b => buildDtos.Add(new BuildDto()
            {
                BuildID = b.BuildID,
                BuildName = b.BuildName,
                BuildDescription = b.BuildDescription,
                Components = b.BuildComponents.Select(bc => new ComponentDto
                {
                    ComponentID = bc.Component.ComponentID,
                    Name = bc.Component.Name,
                    Type = bc.Component.Type,
                    Manufacturer = bc.Component.Manufacturer,
                    Price = bc.Component.Price,
                    ImagePath = bc.Component.ImagePath
                }).ToList()
            }));

            return buildDtos;
        }

        // GET: api/BuildData/FindBuild/5
        [ResponseType(typeof(BuildDto))]
        [HttpGet]
        [Route("api/builddata/findbuild/{id}")]
        public IHttpActionResult FindBuild(int id)
        {
            Build build = db.Builds.Include(b => b.BuildComponents.Select(bc => bc.Component)).FirstOrDefault(b => b.BuildID == id);
            if (build == null)
            {
                return NotFound();
            }

            BuildDto buildDto = new BuildDto()
            {
                BuildID = build.BuildID,
                BuildName = build.BuildName,
                BuildDescription = build.BuildDescription,
                Components = build.BuildComponents.Select(bc => new ComponentDto
                {
                    ComponentID = bc.Component.ComponentID,
                    Name = bc.Component.Name,
                    Type = bc.Component.Type,
                    Manufacturer = bc.Component.Manufacturer,
                    Price = bc.Component.Price,
                    ImagePath = bc.Component.ImagePath
                }).ToList()
            };

            return Ok(buildDto);
        }

        // POST: api/BuildData/AddBuild
        [ResponseType(typeof(Build))]
        [HttpPost]
        public IHttpActionResult AddBuild(BuildDto buildDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Build build = new Build
            {
                BuildName = buildDto.BuildName,
                BuildDescription = buildDto.BuildDescription,
                BuildComponents = new List<BuildComponent>()
            };

            foreach (var componentDto in buildDto.Components)
            {
                var component = db.Components.Find(componentDto.ComponentID);
                if (component != null)
                {
                    build.BuildComponents.Add(new BuildComponent { ComponentID = component.ComponentID, Build = build });
                }
            }

            db.Builds.Add(build);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = build.BuildID }, build);
        }

        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/builddata/updatebuild/{id}")]
        public IHttpActionResult UpdateBuild(int id, BuildDto buildDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buildDto.BuildID)
            {
                return BadRequest();
            }

            Build build = db.Builds.Include(b => b.BuildComponents).FirstOrDefault(b => b.BuildID == id);
            if (build == null)
            {
                return NotFound();
            }

            build.BuildName = buildDto.BuildName;
            build.BuildDescription = buildDto.BuildDescription;

            // Remove existing components
            var existingBuildComponents = build.BuildComponents.ToList();
            foreach (var buildComponent in existingBuildComponents)
            {
                db.BuildComponents.Remove(buildComponent);
            }

            // Add updated components
            foreach (var componentDto in buildDto.Components)
            {
                var component = db.Components.Find(componentDto.ComponentID);
                if (component != null)
                {
                    build.BuildComponents.Add(new BuildComponent { ComponentID = component.ComponentID, BuildID = build.BuildID });
                }
            }

            db.Entry(build).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildExists(id))
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

        // POST: api/BuildData/DeleteBuild/5
        [ResponseType(typeof(Build))]
        [HttpPost]
        [Route("api/builddata/deletebuild/{id}")]
        public IHttpActionResult DeleteBuild(int id)
        {
            Build build = db.Builds.Include(b => b.BuildComponents).FirstOrDefault(b => b.BuildID == id);
            if (build == null)
            {
                return NotFound();
            }

            // Remove the related build components first
            db.BuildComponents.RemoveRange(build.BuildComponents);

            db.Builds.Remove(build);
            db.SaveChanges();

            return Ok(build);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool BuildExists(int id)
        {
            return db.Builds.Count(e => e.BuildID == id) > 0;
        }




    }




}
