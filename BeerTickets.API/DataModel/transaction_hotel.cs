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
    
    public partial class transaction_hotel
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public string hotel_provider { get; set; }
        public string hotel_name { get; set; }
        public string hotel_address { get; set; }
        public string hotel_phone { get; set; }
        public string hotel_city_ { get; set; }
        public string hotel_state { get; set; }
        public string hotel_postal { get; set; }
        public Nullable<decimal> hotel_latitude { get; set; }
        public Nullable<decimal> hotel_longitude { get; set; }
        public string hotel_country { get; set; }
        public Nullable<System.DateTime> hotel_checkin_datetime { get; set; }
        public Nullable<System.DateTime> hotel_checkout_datetime { get; set; }
        public Nullable<decimal> hotel_total { get; set; }
        public Nullable<decimal> hotel_tax { get; set; }
        public int Id { get; set; }
    }
}