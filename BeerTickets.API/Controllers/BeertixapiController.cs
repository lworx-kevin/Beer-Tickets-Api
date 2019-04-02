using BeerTicket.API.BusinessLayer;
using BeerTicket.API.Models;
using Issuance.API.DataModel;
using Issuance.API.Helper;
using Issuance.API.Models;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace BeerTicket.API.Controllers
{
    [RoutePrefix("api/v1.2/issuance")]
    public class IssuanceController : ApiController
    {
        /// <summary>
        /// Authenticate the user and generate the token 
        /// </summary>
        /// <param name="apiUsersViewModel"></param>
        /// <returns></returns>
        [Route("authenticate")]
        [HttpPost]
        public IHttpActionResult Authenticate(ApiUsersViewModel apiUsersViewModel)
        {
            if (apiUsersViewModel != null)
            {
                SunwingVouchersEntities context = new SunwingVouchersEntities();
                IssuanceManager issuanceManager = new IssuanceManager(context);
                var user = context.ApiUsers.Where(x => x.UserName == apiUsersViewModel.UserName &&
                 x.Password == apiUsersViewModel.Password && !x.IsDeleted).FirstOrDefault();

                //  generate and save new token 
                if (user != null)
                {
                    user.Token = issuanceManager.GenerateUniqueToken();
                    user.TokenTimestamp = System.DateTime.Now.AddSeconds(BeerTicketConfigurationManager.TokenLifeTime);
                    user.ModefiedOn = System.DateTime.Now;
                    context.SaveChanges();
                }
                return Ok(new { results = user });
            }
            return Ok();
        }

        /// <summary>
        /// validate and create new voucher 
        /// </summary>
        /// <param name="apiUsersViewModel"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public IHttpActionResult AddNewVoucher(VoucherViewModel voucherViewModel)
        {
            SunwingVouchersEntities context = new SunwingVouchersEntities();
            IssuanceManager issuanceManager = new IssuanceManager(context);
            StringBuilder voucherHtml = new StringBuilder(System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Content/Template/voucherEmail.html")));
            voucherHtml.Replace("@@VoucherNo", "");
            voucherHtml.Replace("@@ValidTill", "");
            //VoucherViewModel voucherViewModel = new VoucherViewModel()
            //{
            //    CampaignId=payLoad.CampaignId
            //};
            // add new campaign 
            var data = issuanceManager.AddVoucherMaster(voucherViewModel);
            //public static string insertVoucherData = @"INSERT INTO [dbo].[VoucherMaster]
            //(				                                   
            // [CampaignId]
            //,[VoucherId]
            //,[FirstName]
            //,[LastName]
            //,[VoucherAmount]       
            //,[IssueDate]
            //,[Expirydate]
            //,[Status]
            //,[Created],VoucherValue, FullName)
            //VALUES
            //(
            // @CampaignId
            //,@NewVoucherCode
            //,@FirstName
            //,@LastName
            //,@VoucherAmount  
            //,getdate()
            //,@Expirydate
            //,@Status
            //,getdate(),@VoucherAmount,@FirstName + ' ' +@LastName )";

            //You will need to validate the payload is correct and complete
            //Reject any payload that does not contain a valid token
            //Reject any payload where token has expired
            //Validate that the campaign ID sent exists
            //if (issuanceManager.IsAddNewVoucherPayloadValid(payLoad) &&
            //    issuanceManager.IsValidToken(payLoad.Token) &&
            //    issuanceManager.IsVoucherCampaignExistById(payLoad.CampaignId))
            //{
            // create the new voucher and use the  data of that  in email and HTML 
            //    var voucherHtml = System.IO.File.ReadAllText("~/Content/Template/voucherEmail.html");
            //    //If deliver = 1, email = 1 and pdf = 0, send the voucher to the email address in HTML format.
            //    //If deliver = 1, email = 1 and pdf = 1, send the voucher to the email address in HTML format, but attached a PDF version of the voucher as we

            //}
            return Ok();
        }
    }
}
