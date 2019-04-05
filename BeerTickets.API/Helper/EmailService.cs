using BeerTicket.API.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BeerTicket.API.Helper
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// this is the common method to send the email used in entity 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsyncWithAttachment(IdentityMessage message, byte[] stream, string fileName)
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

            email.Attachments.Add(new Attachment(new MemoryStream(stream), fileName));
            return mailClient.SendMailAsync(email);
        }
        /// <summary>
        /// this is the simple email sending method for the receiver 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendEmailWithoughtAttachAsync(IdentityMessage message)
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