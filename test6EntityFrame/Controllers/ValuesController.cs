using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace test6EntityFrame.Controllers
{
   
    public class ValuesController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();
        // GET api/values
        [Authorize(Roles = "Cashier")]
        public IHttpActionResult Get()
        {
            var data = db.Modules.ToList();
            return Ok(data);
        }

        // GET api/values/5
        [Authorize(Roles = "Admin")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
