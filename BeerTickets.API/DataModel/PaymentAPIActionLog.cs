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
    
    public partial class PaymentAPIActionLog
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }
}
