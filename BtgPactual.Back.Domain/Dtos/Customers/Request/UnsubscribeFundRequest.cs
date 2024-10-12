using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Customers.Request
{
    [ExcludeFromCodeCoverage]
    public class UnsubscribeFundRequest
    {
        [JsonProperty("customerId", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerId { get; set; }

        [JsonProperty("fundId", NullValueHandling = NullValueHandling.Ignore)]
        public string FundId { get; set; }

        [JsonProperty("transactionId", NullValueHandling = NullValueHandling.Ignore)]
        public string TransactionId { get; set; }
    }
}
