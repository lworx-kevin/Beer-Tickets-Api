//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Issuance.API.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class VoucherMasterBckup
    {
        public int Id { get; set; }
        public Nullable<int> CampaignId { get; set; }
        public string VoucherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<decimal> VoucherAmount { get; set; }
        public string incProduct { get; set; }
        public string exclProduct { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<System.DateTime> Expirydate { get; set; }
        public Nullable<System.DateTime> UseDate { get; set; }
        public string UseProduct { get; set; }
        public string UseRezId { get; set; }
        public Nullable<decimal> UseSaleAmount { get; set; }
        public string UseTaxesIncluded { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> FundingId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DateVoid { get; set; }
        public Nullable<decimal> VoucherValue { get; set; }
    }
}
