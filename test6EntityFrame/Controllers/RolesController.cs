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
    public class RolesController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/Roles
        [Route("api/Roles")]
        public HttpResponseMessage GetAspNetRoles()
        {
            return Request.CreateResponse(HttpStatusCode.OK, db.AspNetRoles);
        }

        [Route("api/RolesById")]
        public HttpResponseMessage GetAspNetRolesById(string id)
        {
            AspNetRoles enitity = db.AspNetRoles.Find(id);
            if (enitity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, enitity);
        }

        // PUT: api/Roles/5
        [Route("api/Roles")]
        public HttpResponseMessage PutAspNetRoles(string id, string roleName)
        {


            var entity = db.AspNetRoles.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }
            else
            {
                entity.Name = roleName;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        [Route("api/Roles")]
        public HttpResponseMessage PostAspNetRoles(string InputPageName)
        {
            var CustomId = Guid.NewGuid().ToString("N");

            var newRole = new AspNetRoles()
            {
                Id = CustomId,
                Name = InputPageName

            };
            db.AspNetRoles.Add(newRole);


            var ListOfPages = from dataTable in db.Pages select dataTable.page_id;
            foreach (int ch in ListOfPages)
            {
                var CustomIdPermission = Guid.NewGuid().ToString("N");

                var newPAgePermission = new PagePermission()
                {

                    PermissionId = CustomIdPermission,
                    RoleId = CustomId,
                    PageId = ch,
                    EditPermission = "false",
                    viewPermission = "false",
                    DelPermission = "false",
                    AddPermission = "false"
                };
                db.PagePermission.Add(newPAgePermission);

            }


            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, newRole);
        }

        [Route("api/Roles")]
        public HttpResponseMessage DeleteAspNetRoles(string id)
        {
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }

            db.AspNetRoles.Remove(aspNetRoles);

            var permissionIds = from prTable in db.PagePermission where prTable.RoleId == id select prTable.PermissionId;
            foreach (string ch in permissionIds)
            {

                PagePermission prPermission = db.PagePermission.Find(ch);
                if (prPermission == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
                }

                db.PagePermission.Remove(prPermission);
            }
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, aspNetRoles);
        }


    }
}