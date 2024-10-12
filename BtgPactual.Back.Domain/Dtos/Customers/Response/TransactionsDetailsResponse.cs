using BtgPactual.Back.Domain.Dtos.Funds;
using BtgPactual.Back.Domain.Dtos.Response;
using Newtonsoft.Json;

namespace BtgPactual.Back.Domain.Dtos.Customers.Response
{
    public class TransactionsDetailsResponse : GenericResponse
    {
        [JsonProperty("availableFunds", NullValueHandling = NullValueHandling.Ignore)]
        public List<FundDto>? AvailableFunds { get; set; } = [];

        [JsonProperty("activeTransactions", NullValueHandling = NullValueHandling.Ignore)]
        public List<TransactionItem>? ActiveTransactions { get; set; } = [];

        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public string Balance { get; set; }

        [JsonProperty("amountInvested", NullValueHandling = NullValueHandling.Ignore)]
        public string AmountInvested { get; set; }
    }
}
