using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace test6EntityFrame.Controllers
{
    public class NavigationController : ApiController
    {
        private db_weavingEntities db = new db_weavingEntities();

        [Authorize]
        [Route("api/navigation")]
        public HttpResponseMessage Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var LogIn = (identity.Claims
                 .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var Name = (identity.Claims
              .FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);
            var RoleName = (identity.Claims
                 .FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);
            //var Email = (identity.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.Email).Value);
            var RoleID = from roleTable in db.AspNetUserRoles
                         where roleTable.UserId == LogIn
                         select roleTable.RoleId; var finalResult = new
                         {
                             LoginName = LogIn,
                             RoleName = RoleName,
                             userName = Name,
                             navigationResult = from moduleRow in db.Modules
                                                select new
                                                {
                                                    moduleRow.module_name,
                                                    moduleRow.module_id,
                                                    moduleRow.module_icon,
                                                    pages = (from PageTable in db.Pages
                                                             join PrTable in db.PagePermission on PageTable.page_id equals PrTable.PageId
                                                             where PrTable.RoleId == RoleID.FirstOrDefault() && PageTable.module_id == moduleRow.module_id
                                                             select new
                                                             {
                                                                 pageName = PageTable.page_name,
                                                                 pageId = PageTable.page_id,
                                                                 pageURL = PageTable.page_link,
                                                                 PrTable.AddPermission,
                                                                 PrTable.DelPermission,
                                                                 PrTable.EditPermission,
                                                                 PrTable.viewPermission
                                                             })
                                                }
                         };

            return Request.CreateResponse(HttpStatusCode.OK, finalResult);
        }


    }
}
