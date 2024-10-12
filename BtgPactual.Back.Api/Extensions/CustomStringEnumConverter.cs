using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public class CustomStringEnumConverter : StringEnumConverter
    {
        private static readonly Type[] _excludeTypes = Array.Empty<Type>();

        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType) && !_excludeTypes.Contains(objectType);
        }
    }
}
