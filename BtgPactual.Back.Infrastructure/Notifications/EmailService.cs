using BtgPactual.Back.Domain.Configurations;
using BtgPactual.Back.Domain.Interfaces.Notifications;
using System.Net.Mail;
using System.Net;

namespace BtgPactual.Back.Infrastructure.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public bool Send(string body, string subject, string destination)
        {
            try
            {
                var fromAddress = new MailAddress(_emailConfiguration.Email, _emailConfiguration.From);
                var toAddress = new MailAddress(destination);

                var smtp = new SmtpClient
                {
                    Host = _emailConfiguration.SmtpHost,
                    Port = _emailConfiguration.SmtpPort,
                    EnableSsl = _emailConfiguration.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = _emailConfiguration.UseDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, _emailConfiguration.Password)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
