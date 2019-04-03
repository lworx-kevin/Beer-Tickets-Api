﻿using Issuance.API.DataModel;
using Issuance.API.Models;
using System;
using System.Linq;

namespace BeerTicket.API.BusinessLayer
{
    public class IssuanceManager
    {
        private SunwingVouchersEntities _dbContext { get; set; }
        public IssuanceManager(SunwingVouchersEntities dbContext)
        {
            _dbContext = dbContext;

        }
        #region methods
        /// <summary>
        /// this  is created an Encripted token 
        /// </summary>
        /// <returns></returns>
        public string CreateToken()
        {
            byte[] gb = Guid.NewGuid().ToByteArray();
            long l = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                l *= ((int)b + 1);
            }
            String RandomNumb = l.ToString().Replace("-", "").Substring(1, 8);
            return Cryption.GetEncryptedSHA256(RandomNumb);
        }

        /// <summary>
        /// Generate Unique Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GenerateUniqueToken()
        {
            // SunwingVouchersEntities dbContext = new SunwingVouchersEntities();
            string uniqueToken = "";
            //generate and check for token 
            while (uniqueToken == "")
            {
                string generatedToken = CreateToken();
                // check for unique token in DB 
                if (!_dbContext.ApiUsers.Where(x => x.Token == generatedToken).Any())
                {
                    uniqueToken = generatedToken;
                }
            }
            return uniqueToken;
        }
        /// <summary>
        /// check for valid payload 
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsAddNewVoucherPayloadValid(VoucherViewModel voucherViewModel)
        {
            //Reject any payload that does not contain a valid token
            if (String.IsNullOrEmpty(voucherViewModel.Token))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// check for token with valid timestamps exist 
        /// </summary>
        /// <param name="payLoad"></param>
        /// <returns></returns>
        public bool IsValidToken(string token)
        {
            SunwingVouchersEntities dbContext = new SunwingVouchersEntities();
            //check for valid token 
            if (!String.IsNullOrEmpty(token) && dbContext.ApiUsers.Where(x => x.Token == token && x.TokenTimestamp > DateTime.Now && !x.IsDeleted).Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// check Voucher Campaign Exists by Id 
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public bool IsVoucherCampaignExistById(int campaignId)
        {
            return _dbContext.VoucherCampaigns.Where(x => x.Id == campaignId && x.Status == 1).Any();
        }

        /// <summary>
        /// create a new voucher based on prefix 
        /// </summary>
        /// <param name="newPrefix"></param>
        /// <returns></returns>
        public string CreateVoucher(string newPrefix)
        {
            byte[] gb = Guid.NewGuid().ToByteArray();
            long l = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                l *= ((int)b + 1);
            }
            String RandomNumb = l.ToString().Replace("-", "").Substring(1, 8);
            Int32 INPrefix; Int32 IRandomNum;
            Int32.TryParse(newPrefix, out INPrefix);
            Int32.TryParse(RandomNumb, out IRandomNum);
            String DigitCode = (((INPrefix + IRandomNum) / INPrefix) % 100).ToString();
            return newPrefix + RandomNumb + DigitCode;
        }
        /// <summary>
        /// add voucher master 
        /// </summary>
        public bool AddVoucherMaster(VoucherViewModel voucherViewModel)
        {

            try
            {
                #region Get Campaign Category Prefix
                //select CC.CouponPrefix from CouponCategory CC inner join VoucherCampaign VC on VC.CategoryId = CC.Id 
                //where VC.Id = @CampaignId
                var couponPrefix = (from cc in _dbContext.CouponCategories
                                    join vc in _dbContext.VoucherCampaigns on
                                    cc.Id equals vc.CategoryId
                                    where vc.Id == voucherViewModel.CampaignId
                                    select cc.CouponPrefix).FirstOrDefault();

                #endregion
                #region Coupon Prefix for Code
                String newPrefix = couponPrefix.Trim();
                #endregion

                #region Pad Campaign Id with zero
                Int32 Length = Convert.ToInt32(voucherViewModel.CampaignId.ToString().Length);
                Int32 DigitToAppend = 5;
                String PCampId;
                if (Length != 0)
                {
                    PCampId = voucherViewModel.CampaignId.ToString();
                }
                else
                {
                    PCampId = "00000";
                }
                while (Length < DigitToAppend)
                {
                    PCampId = "0" + PCampId;
                    Length++;
                }
                #endregion

                #region checklists
                String newVoucherCode = "";
                // check for the voucher code and set new voucher code  
                while (newVoucherCode == "")
                {
                    // check for repeated code in db                
                    string generatedVoucher = CreateVoucher(newPrefix);
                    // check for unique token in DB 
                    if (!_dbContext.VoucherTables.Where(x => x.VoucherCode == generatedVoucher).Any())
                    {
                        newVoucherCode = generatedVoucher;
                        //INSERT INTO VoucherTable(VoucherCode) VALUES(@NewVoucherCode)
                        VoucherTable voucherTable = new VoucherTable
                        {
                            VoucherCode = newVoucherCode
                        };
                        _dbContext.VoucherTables.Add(voucherTable);
                        _dbContext.SaveChanges();
                    }
                }

                #endregion
                VoucherMaster voucherMaster = new VoucherMaster()
                {
                    CampaignId = voucherViewModel.CampaignId,
                    VoucherId = newVoucherCode,
                    FirstName = voucherViewModel.FirstName,
                    LastName = voucherViewModel.LastName,
                    VoucherAmount = voucherViewModel.VoucherAmount,
                    IssueDate = DateTime.Now,
                    Expirydate = voucherViewModel.Expirydate,
                    Status = voucherViewModel.Status,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    VoucherValue = voucherViewModel.VoucherValue,
                    FullName = voucherViewModel.FirstName + voucherViewModel.LastName
                };
                _dbContext.VoucherMasters.Add(voucherMaster);
                _dbContext.SaveChanges();

                // add the added values and return back 
                voucherViewModel.Id = voucherMaster.Id;
                //voucherViewModel.VoucherValue = voucherMaster.VoucherValue;
                voucherViewModel.VoucherId = voucherMaster.VoucherId;
                voucherViewModel.IssueDate = voucherMaster.IssueDate;
                voucherViewModel.FullName = voucherMaster.FullName;
                //DateTime Created = DateTime.Now;
                //DateTime IssueDate = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}