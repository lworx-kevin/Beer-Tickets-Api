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
    
    public partial class CouponFunding
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CouponFunding()
        {
            this.VoucherCampaigns = new HashSet<VoucherCampaign>();
        }
    
        public int Id { get; set; }
        public string GlCode { get; set; }
        public string Department { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public byte[] Updated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoucherCampaign> VoucherCampaigns { get; set; }
    }
}