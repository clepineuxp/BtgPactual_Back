using BtgPactual.Back.Domain.Configurations;
using BtgPactual.Back.Domain.Interfaces.Notifications;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BtgPactual.Back.Infrastructure.Notifications
{
    public class SmsService : ISmsService
    {
        private readonly SmsConfiguration _smsConfiguration;

        public SmsService(SmsConfiguration smsConfiguration)
        {
            _smsConfiguration = smsConfiguration;
        }
        public bool Send(string body, string destination)
        {
            try
            {
                var accountSid = _smsConfiguration.AccountSid;
                var authToken = _smsConfiguration.AuthToken;
                TwilioClient.Init(accountSid, authToken);
                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber(destination));
                messageOptions.MessagingServiceSid = _smsConfiguration.MessagingServiceSid;
                messageOptions.Body = body;
                var message = MessageResource.Create(messageOptions);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
