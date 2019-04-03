using BeerTicket.API.BusinessLayer;
using BeerTicket.API.Models;
using Issuance.API.DataModel;
using Issuance.API.Helper;
using Issuance.API.Models;
using Microsoft.AspNet.Identity;
using OpenHtmlToPdf;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace BeerTicket.API.Controllers
{
    [RoutePrefix("Beertixapi/v1.2")]
    public class BeerTixApiController : Controller
    {
        [HttpPost]
        [Route("authenticate")]
        public ApiUsersViewModel Authenticate(ApiUsersViewModel apiUsersViewModel)
        {
            if (apiUsersViewModel != null)
            {
                SunwingVouchersEntities context = new SunwingVouchersEntities();
                IssuanceManager issuanceManager = new IssuanceManager(context);
                var user = context.ApiUsers.Where(x => x.UserName == apiUsersViewModel.UserName &&
                 x.Password == apiUsersViewModel.Password && !x.IsDeleted).FirstOrDefault();

                apiUsersViewModel.Token = user.Token;
                //  generate and save new token 
                if (user != null)
                {
                    user.Token = issuanceManager.GenerateUniqueToken();
                    user.TokenTimestamp = System.DateTime.Now.AddSeconds(BeerTicketConfigurationManager.TokenLifeTime);
                    user.ModefiedOn = System.DateTime.Now;
                    context.SaveChanges();
                }
            }
            return apiUsersViewModel;
        }
        /// <summary>
        /// validate and create new voucher 
        /// </summary>
        /// <param name="apiUsersViewModel"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<VoucherViewModel> AddNewVoucher() // VoucherViewModel voucherViewModel  
        {
            VoucherViewModel voucherViewModel = new VoucherViewModel();
            try
            {
                #region Testing Data 
                // for testing 
                voucherViewModel.Token = "130E3452920015FEB7EE83AD7B1E80850495C4A0DDA137498E39783DC7355453";
                voucherViewModel.CampaignId = 46;
                voucherViewModel.FirstName = "FirstName";
                voucherViewModel.LastName = "LastName";
                voucherViewModel.Expirydate = System.DateTime.Now.AddDays(10);
                voucherViewModel.VoucherAmount = 100; // same 
                voucherViewModel.VoucherValue = 100;  // same 
                voucherViewModel.Status = "Approved";
                voucherViewModel.Email = "vashishtsunil007@gmail.com";
                voucherViewModel.IsEmailToSend = true;
                voucherViewModel.SendVoucherAsPdf = true;
                #endregion

                SunwingVouchersEntities context = new SunwingVouchersEntities();
                IssuanceManager issuanceManager = new IssuanceManager(context);
                //You will need to validate the payload is correct and complete
                //Reject any payload that does not contain a valid token
                //Reject any payload where token has expired
                //Validate that the campaign ID sent exists
                bool isTokenValid = issuanceManager.IsAddNewVoucherPayloadValid(voucherViewModel) &&
                    issuanceManager.IsValidToken(voucherViewModel.Token);

                if (isTokenValid &&
                    issuanceManager.IsVoucherCampaignExistById(voucherViewModel.CampaignId))
                {
                    // insert data 
                    if (issuanceManager.AddVoucherMaster(voucherViewModel))
                    {
                        StringBuilder voucherHtml = new StringBuilder(System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Content/Template/voucherEmail.html")));
                        voucherHtml.Replace("@@VoucherNo", voucherViewModel.VoucherId);
                        voucherHtml.Replace("@@ValidTill", voucherViewModel.Expirydate.ToString());
                        //Send Email 
                        if (voucherViewModel.IsEmailToSend)
                        {
                            EmailService emailService = new EmailService();
                            IdentityMessage message = new IdentityMessage()
                            {
                                Destination = voucherViewModel.Email,
                                Subject = "Coupon Voucher"
                            };
                            //If deliver = 1, email = 1 and pdf = 1, send the voucher to the email address in HTML format, but attached a PDF version of the voucher as we
                            if (voucherViewModel.SendVoucherAsPdf)
                            {
                                message.Body = voucherHtml.ToString();
                                var pdf = Pdf.From(voucherHtml.ToString()).OfSize(PaperSize.A4).Landscape().Content();
                                await emailService.SendAsyncWithAttachment(message, pdf, Guid.NewGuid().ToString() + ".pdf");
                                // send voucher simple as HTML 
                            }
                            //If deliver = 1, email = 1 and pdf = 0, send the voucher to the email address in HTML format.
                            else
                            {
                                await emailService.SendEmailWithoughtAttachAsync(message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                voucherViewModel.ErrorMsg = ex.Message.ToString();
                return voucherViewModel;
            }
            // all done status success
            voucherViewModel.IsSuccess = true;
            return voucherViewModel;
        }
    }
}



//[Route("issuance")]
//[HttpPost]
//public string Issuance(string action)
//{
//    if (action == "authenticate")
//    {
//        return "Ok";
//    }
//    else
//    {
//        return "Invalid Action";
//    }
//}
//    }
//}

//[RoutePrefix("api/v1.2/issuance")]
//public class IssuanceController : ApiController
//{
//    /// <summary>
//    /// Authenticate the user and generate the token 
//    /// </summary>
//    /// <param name="apiUsersViewModel"></param>
//    /// <returns></returns>
//    [Route("authenticate")]
//    [HttpPost]
//    public IHttpActionResult Authenticate(ApiUsersViewModel apiUsersViewModel)
//    {
//        if (apiUsersViewModel != null)
//        {
//            SunwingVouchersEntities context = new SunwingVouchersEntities();
//            IssuanceManager issuanceManager = new IssuanceManager(context);
//            var user = context.ApiUsers.Where(x => x.UserName == apiUsersViewModel.UserName &&
//             x.Password == apiUsersViewModel.Password && !x.IsDeleted).FirstOrDefault();
//            //  generate and save new token 
//            if (user != null)
//            {
//                user.Token = issuanceManager.GenerateUniqueToken();
//                user.TokenTimestamp = System.DateTime.Now.AddSeconds(BeerTicketConfigurationManager.TokenLifeTime);
//                user.ModefiedOn = System.DateTime.Now;
//                context.SaveChanges();
//            }
//            return Ok(new { results = user });
//        }
//        return Ok();
//    }

