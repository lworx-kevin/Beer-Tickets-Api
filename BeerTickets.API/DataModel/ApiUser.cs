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
    
    public partial class ApiUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Nullable<System.DateTime> TokenTimestamp { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModefiedOn { get; set; }
        public string ModefiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
