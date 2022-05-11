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
    public class employeeDesignationsController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/employeeDesignations
        public IQueryable<employeeDesignation> GetemployeeDesignation()
        {
            return db.employeeDesignation;
        }

        // GET: api/employeeDesignations/5
        [ResponseType(typeof(employeeDesignation))]
        public IHttpActionResult GetemployeeDesignation(int id)
        {
            employeeDesignation employeeDesignation = db.employeeDesignation.Find(id);
            if (employeeDesignation == null)
            {
                return NotFound();
            }

            return Ok(employeeDesignation);
        }

        // PUT: api/employeeDesignations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutemployeeDesignation(int id, employeeDesignation employeeDesignation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeDesignation.designation_id)
            {
                return BadRequest();
            }

            db.Entry(employeeDesignation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeDesignationExists(id))
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

        // POST: api/employeeDesignations
        [ResponseType(typeof(employeeDesignation))]
        public IHttpActionResult PostemployeeDesignation(employeeDesignation employeeDesignation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.employeeDesignation.Add(employeeDesignation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeDesignation.designation_id }, employeeDesignation);
        }

        // DELETE: api/employeeDesignations/5
        [ResponseType(typeof(employeeDesignation))]
        public IHttpActionResult DeleteemployeeDesignation(int id)
        {
            employeeDesignation employeeDesignation = db.employeeDesignation.Find(id);
            if (employeeDesignation == null)
            {
                return NotFound();
            }

            db.employeeDesignation.Remove(employeeDesignation);
            db.SaveChanges();

            return Ok(employeeDesignation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool employeeDesignationExists(int id)
        {
            return db.employeeDesignation.Count(e => e.designation_id == id) > 0;
        }
    }
}