using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Customers
{
    [ExcludeFromCodeCoverage]
    public class CustomerDto : GeneralEntityDto
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public double Balance { get; set; }

        [JsonProperty("transactions", NullValueHandling = NullValueHandling.Ignore)]
        public List<FundTransactionDto> Transactions { get; set; } = [];
    }
}
