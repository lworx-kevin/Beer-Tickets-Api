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
    
    public partial class ProcessVouchersUpload
    {
        public int Id { get; set; }
        public Nullable<int> UploadId { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<bool> IsEmailSent { get; set; }
        public Nullable<bool> IsPdfSent { get; set; }
        public string ErrorMsg { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModefiedOn { get; set; }
        public string ModefiedBy { get; set; }
        public Nullable<bool> IsDeleated { get; set; }
    
        public virtual VouchersUpload VouchersUpload { get; set; }
    }
}