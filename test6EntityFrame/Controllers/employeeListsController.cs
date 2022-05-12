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
    public class employeeListsController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/employeeLists
        public IQueryable<employeeList> GetemployeeList()
        {
            return db.employeeList;
        }

        // GET: api/employeeLists/5
        [ResponseType(typeof(employeeList))]
        public IHttpActionResult GetemployeeList(int id)
        {
            employeeList employeeList = db.employeeList.Find(id);
            if (employeeList == null)
            {
                return NotFound();
            }

            return Ok(employeeList);
        }

        // PUT: api/employeeLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutemployeeList(int id, employeeList employeeList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeList.employee_Id)
            {
                return BadRequest();
            }

            db.Entry(employeeList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeListExists(id))
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

        // POST: api/employeeLists
        [ResponseType(typeof(employeeList))]
        public IHttpActionResult PostemployeeList(employeeList employeeList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.employeeList.Add(employeeList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeList.employee_Id }, employeeList);
        }

        // DELETE: api/employeeLists/5
        [ResponseType(typeof(employeeList))]
        public IHttpActionResult DeleteemployeeList(int id)
        {
            employeeList employeeList = db.employeeList.Find(id);
            if (employeeList == null)
            {
                return NotFound();
            }

            db.employeeList.Remove(employeeList);
            db.SaveChanges();

            return Ok(employeeList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool employeeListExists(int id)
        {
            return db.employeeList.Count(e => e.employee_Id == id) > 0;
        }
    }
}