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
    
    public partial class transaction_passenger
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public string ticket_number { get; set; }
        public string ticket_class { get; set; }
        public Nullable<decimal> total_amount { get; set; }
        public string passenger_dob { get; set; }
        public string passenger_title { get; set; }
        public string passenger_first_ { get; set; }
        public string passenger_middle { get; set; }
        public string passenger_last { get; set; }
        public string passenger_email { get; set; }
        public string passenger_phone { get; set; }
        public string passenger_loyalty { get; set; }
        public int Id { get; set; }
    }
}
