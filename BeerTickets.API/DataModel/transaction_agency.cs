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
    
    public partial class transaction_agency
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public string agency_code { get; set; }
        public string agency_name { get; set; }
        public string agency_address { get; set; }
        public string agency_address2 { get; set; }
        public string agency_postal { get; set; }
        public string agency_city { get; set; }
        public string agency_state { get; set; }
        public string agency_country { get; set; }
        public string agency_email { get; set; }
        public string agency_consortium { get; set; }
        public string agency_type { get; set; }
        public int Id { get; set; }
    }
}
