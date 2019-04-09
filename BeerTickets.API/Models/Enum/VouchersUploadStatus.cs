namespace BeerTicket.API.Models
{
    public enum VouchersUploadStatus
    {
        NotProcessed = 0,
        ProcessedWithSuccess = 1,
        ProcessedWithNotSuccess = 2,

        Issuing = 3,
        IssuedWithSuccess=4,
        IssuedWithNotSucces=5
    }
}