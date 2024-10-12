using BtgPactual.Back.Domain.Dtos.Response;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Customers.Response
{
    [ExcludeFromCodeCoverage]
    public class TransactionFundResponse : GenericResponse
    {
        [JsonProperty("transactionId", NullValueHandling = NullValueHandling.Ignore)]
        public string? TransactionId { get; set; }

        [JsonProperty("newBalance", NullValueHandling = NullValueHandling.Ignore)]
        public string? NewBalance { get; set; }
    }
}
