using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BtgPactual.Back.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionTypeEnum
    {
        [EnumMember(Value = "Apertura")]
        Opening,
        [EnumMember(Value = "Cancelacion")]
        Cancellation
    }
}
