using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Configurations
{
    [ExcludeFromCodeCoverage]
    public class DatabaseConfiguration : BaseConfiguration
    {
        public string DatabaseConnection { get; set; }
        public string DatabaseName { get; set; }
    }
}
