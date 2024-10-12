using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Configurations
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseConfiguration
    {
        public virtual string ConfigKey => GetType().Name;
    }
}
