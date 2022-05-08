//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using DAL;

//namespace test6EntityFrame.Controllers
//{
//    public class PagesController : ApiController
//    {
//        private db_weavingEntities db = new db_weavingEntities();

//        // GET: api/Pages
//        public IHttpActionResult GetPage()
//        {


//            var joinGroup = (
//                from pagesTable in db.Pages
//                join modulesTable in db.Modules on pagesTable.module_id equals modulesTable.module_id
//                where
//pagesTable.module_id == modulesTable.module_id
//                select new
//                {
//                    id = pagesTable.page_id,
//                    name = pagesTable.page_name,
//                    pageUrl = pagesTable.page_link,
//                    moduleId = modulesTable.module_id,
//                    module = modulesTable.module_name
//                });

//            return Ok(joinGroup);

//        }

//        // GET: api/Pages/5
//        [ResponseType(typeof(Pages))]
//        public IHttpActionResult GetPages(int id)
//        {
//            Pages pages = db.Pages.Find(id);
//            if (pages == null)
//            {
//                return NotFound();
//            }

//            return Ok(pages);
//        }

//        // PUT: api/Pages/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutPages(string id, Pages pages)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != pages.page_id)
//            {
//                return BadRequest();
//            }

//            db.Entry(pages).State = EntityState.Modified;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {

//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Pages
//        [ResponseType(typeof(Pages))]
//        public IHttpActionResult PostPages(Pages pages)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.Pages.Add(pages);

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateException)
//            {

//            }

//            return CreatedAtRoute("DefaultApi", new { id = pages.page_id }, pages);
//        }

//        // DELETE: api/Pages/5
//        [ResponseType(typeof(Pages))]
//        public IHttpActionResult DeletePages(int id)
//        {
//            Pages pages = db.Pages.Find(id);
//            if (pages == null)
//            {
//                return NotFound();
//            }

//            db.Pages.Remove(pages);
//            db.SaveChanges();

//            return Ok(pages);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }


//    }
//}