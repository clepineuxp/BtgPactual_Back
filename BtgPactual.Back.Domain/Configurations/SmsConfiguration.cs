using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Configurations
{
    [ExcludeFromCodeCoverage]
    public class SmsConfiguration : BaseConfiguration
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string MessagingServiceSid { get; set; }
    }
}
