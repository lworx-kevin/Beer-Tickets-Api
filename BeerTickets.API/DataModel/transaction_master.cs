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
    
    public partial class transaction_master
    {
        public string token_id { get; set; }
        public Nullable<System.DateTime> create_timestamp { get; set; }
        public Nullable<System.DateTime> last_update_timestamp { get; set; }
        public Nullable<System.DateTime> transmit_timestamp { get; set; }
        public Nullable<bool> transmit_flag { get; set; }
        public int Id { get; set; }
    }
}
