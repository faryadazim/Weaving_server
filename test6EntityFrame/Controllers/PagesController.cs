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

        [Route("api/Pages")]
        public HttpResponseMessage GetPages()
        {
            var joinGroup = (
                from pagesTable in db.Pages
                join modulesTable in db.Modules on pagesTable.module_id equals modulesTable.module_id
                where
                    pagesTable.module_id == modulesTable.module_id
                select new
                {
                    id = pagesTable.page_id,
                    name = pagesTable.page_name,
                    pageUrl = pagesTable.page_link,
                    moduleId = modulesTable.module_id,
                    module = modulesTable.module_name
                });

            return Request.CreateResponse(HttpStatusCode.OK, joinGroup);
        }

        [Route("api/PagesById")]
        public HttpResponseMessage GetPagesById(int id)
        {
            Pages pages = db.Pages.Find(id);
            if (pages == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, pages);
        }

        [Route("api/Pages")]
        public HttpResponseMessage PutPages(Pages pages)
        {

            var entity = db.Pages.FirstOrDefault(e => e.page_id == pages.page_id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }

            db.Entry(pages).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, pages);
        }

        [Route("api/Pages")]
        public HttpResponseMessage PostPages(Pages pages)
        {

            try
            {
                db.Pages.Add(pages);
                db.SaveChanges();

                var ListOfRole = (from dataTAble in db.AspNetRoles select dataTAble.Id);

                foreach (string ch in ListOfRole)
                {

                    var customIdFor = Guid.NewGuid().ToString("P");
                    var newPAgePermission = new PagePermission()
                    {

                        PermissionId = customIdFor,
                        RoleId = ch,
                        PageId = pages.page_id,
                        EditPermission = "false",
                        viewPermission = "false",
                        DelPermission = "false",
                        AddPermission = "false"
                    };
                    db.PagePermission.Add(newPAgePermission);

                }
                db.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, pages);
                message.Headers.Location = new Uri(Request.RequestUri + pages.page_id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/Pages")]
        public HttpResponseMessage DeletePages(int id)
        {
            Pages entity = db.Pages.Find(id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }

            db.Pages.Remove(entity);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }
    }
}