namespace BeerTicket.API.Models
{
    public class PayLoad // like base modal 
    {
        public string Token { get; set; }
        public int CampaignId { get; set; }

        //email section 
        public bool IsEmailToSend { get; set; }
        public bool SendVoucherAsPdf { get; set; }
        public bool IsDeliver { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsFirstRowAsColumnNames { get; set; }

        // for Content for bulk upload 
        public string base64Content { get; set; }
    }
}