using BeerTicket.API.BusinessLayer;
using BeerTicket.API.DataModel;
using BeerTicket.API.Helper;
using BeerTicket.API.Models;
using Microsoft.AspNet.Identity;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
        public async Task<VoucherModel> AddNewVoucher(VoucherModel voucherViewModel)
        {
            //  VoucherViewModel voucherViewModel = new VoucherViewModel();
            try
            {
                //#region Testing Data 
                //// for testing 
                //voucherViewModel.Token = "130E3452920015FEB7EE83AD7B1E80850495C4A0DDA137498E39783DC7355453";
                //voucherViewModel.CampaignId = 46;
                //voucherViewModel.FirstName = "FirstName";
                //voucherViewModel.LastName = "LastName";
                //voucherViewModel.Expirydate = System.DateTime.Now.AddDays(10);
                //voucherViewModel.VoucherAmount = 100; // same 
                //voucherViewModel.VoucherValue = 100;  // same 
                //voucherViewModel.Status = "Approved";
                //voucherViewModel.Email = "vashishtsunil007@gmail.com";
                //voucherViewModel.IsEmailToSend = true;
                //voucherViewModel.SendVoucherAsPdf = true;
                //#endregion

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUsersViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generate")]
        public async Task<VouchersUploadModel> GenerateVouchers(VouchersUploadModel vouchersUploadModel)
        {
            SunwingVouchersEntities context = new SunwingVouchersEntities();
            IssuanceManager issuanceManager = new IssuanceManager(context);

            #region test data 
            //payLoad.Token = "";
            #endregion
            string FileUploadPath = "~/Content/Upload/";
            string RelativeExcelPath = "\\Content\\Upload";
            Byte[] bytes = System.IO.File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Content/Template/export.csv"));
            String base64encodedstring = Convert.ToBase64String(bytes);
            byte[] fileBytes = System.Convert.FromBase64String(base64encodedstring);

            var path = Server.MapPath(FileUploadPath) + Convert.ToString(CultureInfo.InvariantCulture) + "\\";
            string relativeExcelPath = RelativeExcelPath + Convert.ToString(CultureInfo.InvariantCulture) + "\\";
            if (!Directory.Exists(path))
            { // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            //add prefix file upload id on filename for uniqueness
            string fileLocation = path + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + "_" + "Uploaded.csv";
            relativeExcelPath = relativeExcelPath + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + "_" + "Uploaded.csv";

            // if file with same name exists delete that 
            if (System.IO.File.Exists(fileLocation))
            {
                System.IO.File.Delete(fileLocation);
            }
            // write  all the content in csv file 
            using (var fs = new FileStream(fileLocation, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            // save this file to database first step when api will hit the data with base64 string 
            //VouchersUploadModel vouchersUploadModel = new VouchersUploadModel()
            //{
            vouchersUploadModel.UploadContent = fileLocation;
            vouchersUploadModel.CreatedOn = DateTime.Now;
            vouchersUploadModel.Status = (int)VouchersUploadStatus.NotProcessed;
            //};

            // add new voucher upload 
            if (issuanceManager.AddVouchersUpload(vouchersUploadModel))
            {
                //issuanceManager.SaveVouchersUpload();
                // this method will be run by scheduler 
                await SaveCsvToDb(vouchersUploadModel);
                //now read the file content
            }
            return vouchersUploadModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="relativeExcelPath"></param>
        /// <param name="IsFirstRowAsColumnNames"></param>
        /// <returns></returns>
        public async Task<bool> SaveCsvToDb(VouchersUploadModel vouchersUploadModel)
        {
            int successCount = 0;// coupons successfully inserted 
            int notSuccessCount = 0; // coupons not successfully inserted 
            int totalProcessed = 0;// to track the current row of the sheet been iterated 

            SunwingVouchersEntities context = new SunwingVouchersEntities();
            IssuanceManager issuanceManager = new IssuanceManager(context);

            // for testing 
            vouchersUploadModel.IsFirstRowAsColumnNames = true;
            List<string> ErrorMassage = new List<string>();
            try
            {
                using (var reader = new StreamReader(vouchersUploadModel.UploadContent))
                {
                    List<string> csvRows = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        csvRows.Add(values[0]);
                    }
                    // if first row is header remove that 
                    if (vouchersUploadModel.IsFirstRowAsColumnNames && csvRows.Count > 1)
                    {
                        csvRows.RemoveAt(0);
                    }
                    // check for validation for   each row             
                    //read row data one by one
                    foreach (var rowItem in csvRows)
                    {
                        // validate each row 
                        // and save the status to database 
                        // with status 
                        var validationData = CsvRowValidate(rowItem);
                        ProcessVouchersUploadModel processVouchersUploadModel = new ProcessVouchersUploadModel();
                        processVouchersUploadModel.UploadId = vouchersUploadModel.Id;
                        // if no error 
                        if (!validationData.Item1)
                        {
                            var voucher = await AddNewVoucher(validationData.Item3);
                            if (voucher.IsSuccess)
                            {
                                successCount++;
                                processVouchersUploadModel.Status = (int)ProcessVouchersUploadStatus.Success;
                            }
                            else
                            {
                                notSuccessCount++;
                                processVouchersUploadModel.Status = (int)ProcessVouchersUploadStatus.NotSuccess;
                                processVouchersUploadModel.ErrorMsg = voucher.ErrorMsg;
                            }
                            //save the status in ProcessVouchersUpload  with status success
                        }
                        else
                        {
                            notSuccessCount++;
                            //save the status in ProcessVouchersUpload with status not success 
                            processVouchersUploadModel.ErrorMsg = "Row No " + totalProcessed + "process with error " + processVouchersUploadModel.ErrorMsg;
                        }
                        issuanceManager.AddProcessVouchersUpload(processVouchersUploadModel);
                        totalProcessed++;
                    }
                }

                //
                vouchersUploadModel.Status = (int)VouchersUploadStatus.ProcessedWithSuccess;
                vouchersUploadModel.SuccessfullyProcessed = successCount;
                vouchersUploadModel.NotSuccessfullyProcessed = notSuccessCount;
                vouchersUploadModel.TotalProcessed = totalProcessed;
                issuanceManager.AddVouchersUpload(vouchersUploadModel);
                return true;
            }
            catch (Exception ex)
            {
                // add total process and unprocessed 
                vouchersUploadModel.Status = (int)VouchersUploadStatus.ProcessedWithNotSuccess;
                issuanceManager.AddVouchersUpload(vouchersUploadModel);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, List<string>, VoucherModel> CsvRowValidate(string rowItem)        {
            var voucher = rowItem.Split(',').ToArray();
            bool isError = false;
            VoucherModel voucherViewModel = new VoucherModel();
            List<string> errorMassage = new List<string>();
            try            {                if (string.IsNullOrEmpty(voucher[1]))                {
                    isError = true;                    errorMassage.Add("FullName Required");                }
                if (string.IsNullOrEmpty(voucher[7]))
                {
                    isError = true;                    errorMassage.Add("VoucherValue Required");
                }
                if (!Utilities.IsValidDatetime(voucher[6]))
                {
                    isError = true;                    errorMassage.Add("Expiry Date is not in Right Format");

                }
                if (!isError)
                {
                    //var voucher = rowItem.Split(',').ToArray();
                    #region Testing Data 
                    // for testing 
                    voucherViewModel.Token = "130E3452920015FEB7EE83AD7B1E80850495C4A0DDA137498E39783DC7355453";
                    voucherViewModel.CampaignId = 46;
                    voucherViewModel.FirstName = voucher[15];
                    voucherViewModel.LastName = voucher[16];
                    voucherViewModel.FullName = voucher[0];
                    voucherViewModel.Expirydate = Convert.ToDateTime(voucher[6]);//System.DateTime.Now;//Convert.ToDateTime(voucher[6]);
                    voucherViewModel.VoucherAmount = Decimal.Parse(voucher[7]); // same 
                    voucherViewModel.VoucherValue = Decimal.Parse(voucher[7]);  // same 
                    voucherViewModel.Status = "Approved";
                    voucherViewModel.Email = "vashishtsunil007@gmail.com";
                    voucherViewModel.IsEmailToSend = true;
                    voucherViewModel.SendVoucherAsPdf = true;
                    voucherViewModel.IssueDate = System.DateTime.Now; //Convert.ToDateTime(voucher[5]);
                    voucherViewModel.UseDate = System.DateTime.Now; //Convert.ToDateTime(voucher[12]);
                    #endregion
                }
            }            catch (Exception ex)            {
            }
            return Tuple.Create(isError, errorMassage, voucherViewModel);
        }


    }
}
