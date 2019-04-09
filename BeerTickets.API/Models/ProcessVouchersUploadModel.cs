using System;

namespace BeerTicket.API.Models
{
    public class ProcessVouchersUploadModel : PayLoad
    {
        public int Id { get; set; }
        public Nullable<int> UploadId { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<bool> IsEmailSent { get; set; }
        public Nullable<bool> IsPdfSent { get; set; }
        //public string ErrorMsg { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModefiedOn { get; set; }
        public string ModefiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string CsvContent { get; set; }
    }
}