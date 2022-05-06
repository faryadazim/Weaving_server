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
                          

        // GET: api/Users

            //path -- , role , crud permission **  
            //condition -->: r==true 

      //   [Authorize (Roles ="role")]
        [Route("api/values11")]
        public IHttpActionResult Getvalues11()
        {
            var data = db.AspNetUsers; 

            var user = new
            {
                name = "ali ",
                roll = "22"
            };
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [Route("api/values22")]
        public IHttpActionResult Getvalues22()
        {
            var data = db.AspNetUsers;


            var user = new
            {
                name = "khan ",
                roll = "33"
            };
            return Ok(user);
        }
    }
}
