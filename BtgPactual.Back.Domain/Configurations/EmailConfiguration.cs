using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Configurations
{
    [ExcludeFromCodeCoverage]
    public class EmailConfiguration : BaseConfiguration
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}
