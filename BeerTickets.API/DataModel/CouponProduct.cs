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
    
    public partial class CouponProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public byte[] Updated { get; set; }
        public Nullable<int> ProductTypeId { get; set; }
    }
}
