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
    
    public partial class transaction_excursion
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public string excursion_name { get; set; }
        public string excursion_supplier { get; set; }
        public string excursion_location { get; set; }
        public Nullable<System.DateTime> excursion_datetime { get; set; }
        public Nullable<decimal> excursion_amount { get; set; }
        public Nullable<decimal> excursion_tax { get; set; }
        public int Id { get; set; }
    }
}
