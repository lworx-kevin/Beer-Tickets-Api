namespace Issuance.API.Models
{
    public class PayLoad
    {
        public string Token { get; set; }
        public int CampaignId { get; set; }

        public bool IsEmailToSend { get; set; }
        public bool SendVoucherAsPdf { get; set; }
        public bool IsDeliver { get; set; }
    }
}