using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using test6EntityFrame.Models;
using WebApplication5.Models;

namespace test6EntityFrame.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationUserManager _userManager;
        private db_weavingEntities db = new db_weavingEntities();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/Users
        [Route("api/Users")]
        public IHttpActionResult GetAspNetUsers()
        {
        var data= db.AspNetUsers;
             

            var user = (from uRole in db.AspNetUserRoles
                        join userDB in db.AspNetUsers on uRole.UserId equals userDB.Id
                        from Role in db.AspNetRoles where Role.Id  ==  uRole.RoleId 
                        select new
                        {
                            id = userDB.Id, 
                            email = userDB.Email,
                            userName = userDB.UserName,
                            phoneNumber = userDB.PhoneNumber,
                            passwordHash = userDB.PasswordHash,
                            role = uRole.RoleId,
                            roleName = Role.Name
                        });
            return Ok(user);
        }

        // GET: api/Users/5
        [ResponseType(typeof(AspNetUsers))]
        public IHttpActionResult GetAspNetUsers(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return Ok(aspNetUsers);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAspNetUsers(string id, AspNetUsers aspNetUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetUsers.Id)
            {
                return BadRequest();
            }

            db.Entry(aspNetUsers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUsersExists(id))
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

        // POST: api/Users
        //[ResponseType(typeof(AspNetUsers))]
        //public IHttpActionResult PostAspNetUsers(AspNetUsers aspNetUsers)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AspNetUsers.Add(aspNetUsers);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AspNetUsersExists(aspNetUsers.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = aspNetUsers.Id }, aspNetUsers);
        //}

             
        [Route("api/Users")]
        public IHttpActionResult async(UserDataModel model)
       // public async Task<IHttpActionResult> Register(int param_ID  )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() {Id = Guid.NewGuid().ToString("N"), UserName = model.userName, Email = model.email  , PhoneNumber = model.phoneNumber  };

            IdentityResult result =  UserManager.Create(user,model.password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            var groupJoin = new
            {
                userName = user.UserName,
                userId = user.Id,
                email = user.Email,
                phoneNumber = user.PhoneNumber

            };

            return Ok(groupJoin);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(AspNetUsers))]
        public IHttpActionResult DeleteAspNetUsers(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();

            return Ok(aspNetUsers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUsersExists(string id)
        {
            return db.AspNetUsers.Count(e => e.Id == id) > 0;
        }
    }
}