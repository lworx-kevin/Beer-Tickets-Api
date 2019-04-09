using System;

namespace BeerTicket.API.Models
{
    public class VouchersUploadModel : PayLoad
    {
        public int Id { get; set; }
        public string UploadContent { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModefiedOn { get; set; }
        public string ModefiedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public Nullable<int> SuccessfullyProcessed { get; set; }
        public Nullable<int> NotSuccessfullyProcessed { get; set; }
        public Nullable<int> TotalProcessed { get; set; }

        public string PayloadContent { get; set; }
    }
}