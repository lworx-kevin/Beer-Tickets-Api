using BeerTicket.API.Models;
using Issuance.API.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Issuance.API.Controllers
{
    [RoutePrefix("Beertixapi/v1.2")]
    public class BeerTixApiController : Controller
    {
        [HttpPost]
        public object Authenticate(ApiUsersViewModel apiUsersViewModel)
        {
            SunwingVouchersEntities dbContext = new SunwingVouchersEntities();
            var user = dbContext.ApiUsers.Where(x => x.UserName == apiUsersViewModel.UserName &&
             x.Password == apiUsersViewModel.Password && !x.IsDeleted).FirstOrDefault();
            if (user != null)
            {
                // make the tocken and add the value of expireis  from WebConfig 
            }

            return new object
            {

            };
        }

        [Route("issuance")]
        [HttpPost]
        public string Issuance(string action)
        {
            if (action == "authenticate")
            {
                return "Ok";
            }
            else
            {
                return "Invalid Action";
            }
        }
    }
}