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
    public class PagesController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/Pages
        public IQueryable<Pages> GetPages()
        {
            return db.Pages;
        }

        // GET: api/Pages/5
        [ResponseType(typeof(Pages))]
        public IHttpActionResult GetPages(int id)
        {
            Pages pages = db.Pages.Find(id);
            if (pages == null)
            {
                return NotFound();
            }

            return Ok(pages);
        }

        // PUT: api/Pages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPages(int id, Pages pages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pages.page_id)
            {
                return BadRequest();
            }

            db.Entry(pages).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagesExists(id))
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

        // POST: api/Pages
        [ResponseType(typeof(Pages))]
        public IHttpActionResult PostPages(Pages pages)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pages.Add(pages);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PagesExists(pages.page_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pages.page_id }, pages);
        }

        // DELETE: api/Pages/5
        [ResponseType(typeof(Pages))]
        public IHttpActionResult DeletePages(int id)
        {
            Pages pages = db.Pages.Find(id);
            if (pages == null)
            {
                return NotFound();
            }

            db.Pages.Remove(pages);
            db.SaveChanges();

            return Ok(pages);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagesExists(int id)
        {
            return db.Pages.Count(e => e.page_id == id) > 0;
        }
    }
}