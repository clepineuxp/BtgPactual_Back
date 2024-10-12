using BtgPactual.Back.Domain.Enums;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Customers
{
    [ExcludeFromCodeCoverage]
    public class FundTransactionDto : BaseFundTransactionDto
    {
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BaseFundTransactionDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("fundId", NullValueHandling = NullValueHandling.Ignore)]
        public string FundId { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TransactionTypeEnum Type { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double Amount { get; set; }
    }

}
