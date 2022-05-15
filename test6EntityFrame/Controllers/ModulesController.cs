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
    public class ModulesController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();
        [Route("api/Modules")]
        public HttpResponseMessage GetModules()
        {
            return Request.CreateResponse(HttpStatusCode.OK, db.Modules);

        }
        [Route("api/ModulesById")]
        public HttpResponseMessage GetModulesById(int id)
        {
            Modules entity = db.Modules.Find(id);
            if (entity == null)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        [Route("api/Modules")]
        public HttpResponseMessage PutModules(Modules modules)
        {
            db.Entry(modules).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, modules);
        }

        [Route("api/Modules")]
        public HttpResponseMessage PostModules(Modules modules)
        {

            try
            {
                db.Modules.Add(modules);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, modules);
                message.Headers.Location = new Uri(Request.RequestUri + modules.module_id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [Route("api/Modules")]
        public HttpResponseMessage DeleteModules(int id)
        {
            Modules entity = db.Modules.Find(id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }

            db.Modules.Remove(entity);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

    }
}