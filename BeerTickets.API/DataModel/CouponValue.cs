//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeerTicket.API.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class CouponValue
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public byte[] Updated { get; set; }
    }
}
