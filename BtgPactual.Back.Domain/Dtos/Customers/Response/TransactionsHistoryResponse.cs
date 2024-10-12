using BtgPactual.Back.Domain.Dtos.Response;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Customers.Response
{
    [ExcludeFromCodeCoverage]
    public class TransactionsHistoryResponse : GenericResponse
    {
        [JsonProperty("transactionsCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? TransactionsCount { get; set; }

        [JsonProperty("transactions", NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionItem>? Transactions { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TransactionItem : BaseFundTransactionDto
    {
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public string Date { get; set; }

        [JsonProperty("fundName", NullValueHandling = NullValueHandling.Ignore)]
        public string FundName { get; set; }
    }
}
