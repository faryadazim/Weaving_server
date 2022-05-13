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
    public class BorderQualityController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

    
        [Route("api/BorderQuality")]
        public HttpResponseMessage GetBorderQuality()
        {
            
            return Request.CreateResponse(HttpStatusCode.OK, db.BorderQuality);
        }

        //[Route("api/BorderQuality")]
        public HttpResponseMessage GetBorderQuality(int id)
        {
            BorderQuality borderQuality = db.BorderQuality.Find(id);
            if (borderQuality == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Row not Found");
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.OK, borderQuality);
            }

        }

        [Route("api/BorderQuality")]
        public HttpResponseMessage PutBorderQuality(BorderQuality borderQuality)
        {


            try
            {
                using (db_weavingEntities db = new db_weavingEntities())
                {
                    var entity = db.BorderQuality.FirstOrDefault(e => e.borderQuality_id == borderQuality.borderQuality_id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Row not Found");
                    }
                    else
                    {
                        entity.borderQuality_id = borderQuality.borderQuality_id;
                        entity.borderQuality1 = borderQuality.borderQuality1;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }




        }










        [Route("api/BorderQuality")]
        public HttpResponseMessage PostBorderQuality(BorderQuality borderQualityForPost)
        {


            db.BorderQuality.Add(borderQualityForPost);
            db.SaveChanges();

            var message = Request.CreateResponse(HttpStatusCode.Created, borderQualityForPost);
            message.Headers.Location
 = new Uri(Request.RequestUri + borderQualityForPost.borderQuality_id.ToString());
            return message;
        }






        [Route("api/BorderQuality")]
        public IHttpActionResult DeleteBorderQuality(int id)
        {
            BorderQuality entity = db.BorderQuality.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            db.BorderQuality.Remove(entity);
            db.SaveChanges();

            return Ok(entity);
        }

    }
}