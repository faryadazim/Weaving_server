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
    public class PagePermissionsController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();
        [Route("api/PagePermissions")]
        public IHttpActionResult GetPagePermission(string roleId)
        {
            var pagePermission = from moduleRow in db.Modules
                                 select new
                                 {
                                     moduleRow.module_name,
                                     moduleRow.module_id,
                                     pages = (from PageTable in db.Pages
                                              join PrTable in db.PagePermission on PageTable.page_id equals PrTable.PageId
                                              where PrTable.RoleId == roleId && PageTable.module_id == moduleRow.module_id
                                              select new
                                              {
                                                  pageName = PageTable.page_name,
                                                  pageID = PageTable.page_id,
                                                  pageURL = PageTable.page_link,
                                                  PageTable.page_id,
                                                  //---- Permission Against Role 
                                                  PrTable.AddPermission,
                                                  PrTable.DelPermission,
                                                  PrTable.EditPermission,
                                                  PrTable.viewPermission
                                              })
                                 };
            return Ok(pagePermission);

        }
        [Route("api/PagePermissions")]
        public HttpResponseMessage PutPagePermission(PagePermission pagePermission)
        {
            db.Entry(pagePermission).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, pagePermission);
        }
    }
}