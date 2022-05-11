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
    public class LoomListsController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/LoomLists
        public IQueryable<LoomList> GetLoomList()
        {
            return db.LoomList;
        }

        // GET: api/LoomLists/5
        [ResponseType(typeof(LoomList))]
        public IHttpActionResult GetLoomList(int id)
        {
            LoomList loomList = db.LoomList.Find(id);
            if (loomList == null)
            {
                return NotFound();
            }

            return Ok(loomList);
        }

        // PUT: api/LoomLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoomList(int id, LoomList loomList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loomList.loom_id)
            {
                return BadRequest();
            }

            db.Entry(loomList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoomListExists(id))
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

        // POST: api/LoomLists
        [ResponseType(typeof(LoomList))]
        public IHttpActionResult PostLoomList(LoomList loomList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoomList.Add(loomList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = loomList.loom_id }, loomList);
        }

        // DELETE: api/LoomLists/5
        [ResponseType(typeof(LoomList))]
        public IHttpActionResult DeleteLoomList(int id)
        {
            LoomList loomList = db.LoomList.Find(id);
            if (loomList == null)
            {
                return NotFound();
            }

            db.LoomList.Remove(loomList);
            db.SaveChanges();

            return Ok(loomList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoomListExists(int id)
        {
            return db.LoomList.Count(e => e.loom_id == id) > 0;
        }
    }
}