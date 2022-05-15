﻿using System;
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
    public class weavingUnitsController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        // GET: api/weavingUnits
        public HttpResponseMessage GetweavingUnit()
        {

            return Request.CreateResponse(HttpStatusCode.OK, db.weavingUnit);
        }

        // GET: api/weavingUnits/5
        [ResponseType(typeof(weavingUnit))]
        public HttpResponseMessage GetweavingUnit(int id)
        {
            weavingUnit entity = db.weavingUnit.Find(id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        // PUT: api/weavingUnits/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutweavingUnit(weavingUnit weavingUnit)
        {


            try
            {
                using (db_weavingEntities db = new db_weavingEntities())
                {
                    var entity = db.weavingUnit.FirstOrDefault(e => e.weavingUnit_id == weavingUnit.weavingUnit_id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
                    }
                    else
                    {
                        entity.weavingUnit_id = weavingUnit.weavingUnit_id;
                        entity.weavingUnitName = weavingUnit.weavingUnitName; //here quality1 mean quality name
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

        // POST: api/weavingUnits
        [ResponseType(typeof(weavingUnit))]
        public HttpResponseMessage PostweavingUnit(weavingUnit weavingUnitForPost)
        {
            try
            {
                db.weavingUnit.Add(weavingUnitForPost);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, weavingUnitForPost);
                message.Headers.Location = new Uri(Request.RequestUri + weavingUnitForPost.weavingUnit_id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // DELETE: api/weavingUnits/5
        [ResponseType(typeof(weavingUnit))]
        public HttpResponseMessage DeleteweavingUnit(int id)
        {

            weavingUnit entity = db.weavingUnit.Find(id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record not Found");
            }

            db.weavingUnit.Remove(entity);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

    }
}