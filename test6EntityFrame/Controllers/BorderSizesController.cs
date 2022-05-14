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
    public class BorderSizesController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();
 
        [Route("api/BorderSizes")]
        public HttpResponseMessage GetBorderSize()
        {
             
            return Request.CreateResponse(HttpStatusCode.OK, db.BorderSize);
        }
         
        //[Route("api/BorderQuality")]
    
        public IHttpActionResult GetBorderSize(int id)
        {
            BorderSize borderSize = db.BorderSize.Find(id);
            if (borderSize == null)
            {
                return NotFound();
            }

            return Ok(borderSize);
        }

        // PUT: api/BorderSizes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBorderSize(int id, BorderSize borderSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != borderSize.borderSize_id)
            {
                return BadRequest();
            }

            db.Entry(borderSize).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorderSizeExists(id))
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

        // POST: api/BorderSizes
        [ResponseType(typeof(BorderSize))]
        public IHttpActionResult PostBorderSize(BorderSize borderSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BorderSize.Add(borderSize);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = borderSize.borderSize_id }, borderSize);
        }

        // DELETE: api/BorderSizes/5
        [ResponseType(typeof(BorderSize))]
        public IHttpActionResult DeleteBorderSize(int id)
        {
            BorderSize borderSize = db.BorderSize.Find(id);
            if (borderSize == null)
            {
                return NotFound();
            }

            db.BorderSize.Remove(borderSize);
            db.SaveChanges();

            return Ok(borderSize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BorderSizeExists(int id)
        {
            return db.BorderSize.Count(e => e.borderSize_id == id) > 0;
        }
    }
}