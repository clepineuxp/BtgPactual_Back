using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Customers.Response
{
    [ExcludeFromCodeCoverage]
    public class CustomerInfoResponse : GeneralEntityDto
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public double Balance { get; set; }
    }
}
