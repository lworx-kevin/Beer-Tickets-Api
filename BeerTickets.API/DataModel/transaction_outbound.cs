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
    
    public partial class transaction_outbound
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public string airline_name { get; set; }
        public string flight_number { get; set; }
        public Nullable<System.DateTime> departure_datetime { get; set; }
        public string departure_city { get; set; }
        public string departure_airport { get; set; }
        public Nullable<System.DateTime> arrival_datetime { get; set; }
        public string arrival_city { get; set; }
        public string arrival_airport { get; set; }
        public int Id { get; set; }
    }
}
