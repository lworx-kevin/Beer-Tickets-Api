using BeerTicket.API.Models;
using Issuance.API.DataModel;
using System.Linq;
using System.Web.Http;

namespace BeerTicket.API.Controllers
{
    //http://domain.com/api/v1.2/issuance
    [RoutePrefix("api/v1.2/issuance")]
    public class IssuanceController : ApiController
    {
        [Route("authenticate")]
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
    }
}
