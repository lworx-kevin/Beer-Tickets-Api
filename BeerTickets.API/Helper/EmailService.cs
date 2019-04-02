using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Issuance.API.Helper
{
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// this is the common method to send the email used in entity 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage email = new MailMessage(
                BeerTicketConfigurationManager.EmailFrom,
                message.Destination);
            email.Subject = message.Subject;
            email.Body = message.Body;
            email.IsBodyHtml = true;
            var mailClient = new SmtpClient(BeerTicketConfigurationManager.EmailHost,
                BeerTicketConfigurationManager.EmailPort)
            {
                Credentials = new NetworkCredential(
                      BeerTicketConfigurationManager.EmailUserName,
                      BeerTicketConfigurationManager.EmailPassword
                   ),
                EnableSsl = true
            };
            return mailClient.SendMailAsync(email);
        }
    }
}