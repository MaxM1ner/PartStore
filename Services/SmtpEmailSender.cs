using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace StoreUI.Services
{
    public sealed class SmtpEmailSender : IEmailSender
    {
        private readonly int SmtpClientPort;
        private readonly string SmtpClientHost;
        private readonly string SmtpClientCredentialsUserName;
        private readonly string SmtpClientCredentialsPassword;
        private readonly NetworkCredential NetworkCredentials;
        private readonly string MailAddressFrom;
        public SmtpEmailSender(int smtpClientPort, string smtpClientHost, string smtpClientCredentialsUserName, string smtpClientCredentialsPassword, string mailAddressFrom)
        {
            SmtpClientPort = smtpClientPort;
            SmtpClientHost = smtpClientHost;
            SmtpClientCredentialsUserName = smtpClientCredentialsUserName;
            SmtpClientCredentialsPassword = smtpClientCredentialsPassword;
            NetworkCredentials = new NetworkCredential(smtpClientCredentialsUserName, smtpClientCredentialsPassword);
            MailAddressFrom = mailAddressFrom;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            message.From = new MailAddress(MailAddressFrom);
            message.To.Add(email);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlMessage;

            smtpClient.Port = SmtpClientPort;
            smtpClient.Host = SmtpClientHost;

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = NetworkCredentials;
            smtpClient.EnableSsl = true;
            
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            return smtpClient.SendMailAsync(message);
        }
    }
}
