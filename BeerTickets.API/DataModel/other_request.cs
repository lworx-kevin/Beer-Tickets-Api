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
    
    public partial class other_request
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> preauth_timestamp { get; set; }
        public Nullable<System.DateTime> force_timestamp { get; set; }
        public string fullname { get; set; }
        public string type { get; set; }
        public int Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
