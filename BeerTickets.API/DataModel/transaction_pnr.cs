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
    
    public partial class transaction_pnr
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public string pnr_number { get; set; }
        public Nullable<decimal> pnr_total { get; set; }
        public Nullable<decimal> pnr_extra_fees { get; set; }
        public string pnr_trip_type { get; set; }
        public Nullable<bool> pnr_group_flag { get; set; }
        public int Id { get; set; }
    }
}