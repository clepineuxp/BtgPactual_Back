using BtgPactual.Back.Domain.Dtos.Customers.Request;
using BtgPactual.Back.Domain.Dtos.Customers.Response;

namespace BtgPactual.Back.Domain.Interfaces.Services
{
    public interface IFundsService
    {
        Task<TransactionFundResponse> SubscribeFund(SubscribeFundRequest subscribeFundRequest, CancellationToken cancellationToken = default);
        Task<TransactionFundResponse> UnsubscribeFund(UnsubscribeFundRequest unsubscribeFundRequest, CancellationToken cancellationToken = default);
        Task<TransactionsHistoryResponse> GetTransactionHistory(string customerId, CancellationToken cancellationToken = default);
        Task<TransactionsDetailsResponse> GetTransactionsDetails(string customerId, CancellationToken cancellationToken = default);
        Task<long> CreateInitialFunds(CancellationToken cancellationToken = default);
        Task<long> VerifyFunds(CancellationToken cancellationToken = default);
    }
}
