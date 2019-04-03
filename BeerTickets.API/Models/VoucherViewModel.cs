using System;

namespace Issuance.API.Models
{
    public class VoucherViewModel : PayLoad
    {
        public int Id { get; set; }
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
        public string Generation_Id { get; set; }
        public string FullName { get; set; }

    }
}