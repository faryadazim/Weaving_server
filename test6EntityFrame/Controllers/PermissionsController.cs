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
    public class PermissionsController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/Permissions
        public IQueryable<PagePermission> GetPagePermission()
        {
            return db.PagePermission;
        }

        // GET: api/Permissions/5
        [ResponseType(typeof(PagePermission))]
        public IHttpActionResult GetPagePermission(int id)
        {
            PagePermission pagePermission = db.PagePermission.Find(id);
            if (pagePermission == null)
            {
                return NotFound();
            }

            return Ok(pagePermission);
        }

        // PUT: api/Permissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPagePermission(int id, PagePermission pagePermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pagePermission.PermissionId)
            {
                return BadRequest();
            }

            db.Entry(pagePermission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagePermissionExists(id))
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

        // POST: api/Permissions
        [ResponseType(typeof(PagePermission))]
        public IHttpActionResult PostPagePermission(PagePermission pagePermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PagePermission.Add(pagePermission);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PagePermissionExists(pagePermission.PermissionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pagePermission.PermissionId }, pagePermission);
        }

        // DELETE: api/Permissions/5
        [ResponseType(typeof(PagePermission))]
        public IHttpActionResult DeletePagePermission(int id)
        {
            PagePermission pagePermission = db.PagePermission.Find(id);
            if (pagePermission == null)
            {
                return NotFound();
            }

            db.PagePermission.Remove(pagePermission);
            db.SaveChanges();

            return Ok(pagePermission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagePermissionExists(int id)
        {
            return db.PagePermission.Count(e => e.PermissionId == id) > 0;
        }
    }
}