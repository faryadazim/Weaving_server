using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace test6EntityFrame.Controllers
{
    public class ModulesController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/Modules
        public IQueryable<Modules> GetModules()
        {
            return db.Modules;
        }

        // GET: api/Modules/5
        [ResponseType(typeof(Modules))]
        public IHttpActionResult GetModules(int id)
        {
            Modules modules = db.Modules.Find(id);
            if (modules == null)
            {
                return NotFound();
            }

            return Ok(modules);
        }

        // PUT: api/Modules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModules(int id, Modules modules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modules.module_id)
            {
                return BadRequest();
            }

            db.Entry(modules).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModulesExists(id))
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

        // POST: api/Modules
        [ResponseType(typeof(Modules))]
        public IHttpActionResult PostModules(Modules modules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Modules.Add(modules);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ModulesExists(modules.module_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = modules.module_id }, modules);
        }

        // DELETE: api/Modules/5
        [ResponseType(typeof(Modules))]
        public IHttpActionResult DeleteModules(int id)
        {
            Modules modules = db.Modules.Find(id);
            if (modules == null)
            {
                return NotFound();
            }

            db.Modules.Remove(modules);
            db.SaveChanges();

            return Ok(modules);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModulesExists(int id)
        {
            return db.Modules.Count(e => e.module_id == id) > 0;
        }
    }
}