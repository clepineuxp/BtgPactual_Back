using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BtgPactual.Back.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FundCategoryEnum
    {
        [EnumMember(Value = "FPV")]
        FPV,
        [EnumMember(Value = "FIC")]
        FIC
    }
}
