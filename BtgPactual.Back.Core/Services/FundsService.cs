using AutoMapper;
using BtgPactual.Back.Core.Helpers;
using BtgPactual.Back.Domain.Constants;
using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Dtos.Customers.Request;
using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Dtos.Funds;
using BtgPactual.Back.Domain.Enums;
using BtgPactual.Back.Domain.Interfaces.Repositories;
using BtgPactual.Back.Domain.Interfaces.Services;
using MongoDB.Bson;
using System.Net;

namespace BtgPactual.Back.Core.Services
{
    public class FundsService : IFundsService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IFundRepository _fundRepository;
        private readonly IMapper _mapper;

        public FundsService(ICustomerRepository customerRepository, IFundRepository fundRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _fundRepository = fundRepository;
            _mapper = mapper;
        }

        public async Task<TransactionFundResponse> SubscribeFund(SubscribeFundRequest subscribeFundRequest, CancellationToken cancellationToken = default)
        {
            (var transactionResult, var customer, var fund) = await VerifyCustomerAndFund(subscribeFundRequest.CustomerId, subscribeFundRequest.FundId, cancellationToken);
            
            if (transactionResult is not null) { return transactionResult; }

            double requestedAmount = subscribeFundRequest.Amount is null || subscribeFundRequest.Amount <= 0 ? fund.MinimumAmount : subscribeFundRequest.Amount.Value;
            if (customer.Balance < requestedAmount)
            {
                return new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = string.Format(Constants.SubscribeFund.NoBalanceAvailable, fund.Name) };
            }
            if (requestedAmount < fund.MinimumAmount)
            {
                return new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = string.Format(Constants.SubscribeFund.NotMinimumAmount, fund.Name, fund.MinimumAmount.FormatToColombianCurrency()) };
            }

            customer.Balance -= requestedAmount;
            var newTransaction = new FundTransactionDto
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Active = true,
                Amount = requestedAmount,
                Date = DateTime.Now,
                FundId = fund.Id,
                Type = TransactionTypeEnum.Opening
            };
            customer.Transactions.Add(newTransaction);

            var result = await _customerRepository.Update(customer, cancellationToken);

            var response = new TransactionFundResponse();
            if (result is null)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = string.Format(Constants.SubscribeFund.Error, fund.Name);
            }
            else
            {
                response.Status = HttpStatusCode.OK;
                response.Message = string.Format(Constants.SubscribeFund.Subscribed, fund.Name);
                response.TransactionId = newTransaction.Id;
                response.NewBalance = customer.Balance.FormatToColombianCurrency();
            }
            return response;
        }

        public async Task<TransactionFundResponse> UnsubscribeFund(UnsubscribeFundRequest unsubscribeFundRequest, CancellationToken cancellationToken = default)
        {
            (var transactionResult, var customer, var fund) = await VerifyCustomerAndFund(unsubscribeFundRequest.CustomerId, unsubscribeFundRequest.FundId, cancellationToken);

            if (transactionResult is not null) { return transactionResult; }

            var transactionDto = customer.Transactions.Find(t=> t.Id == unsubscribeFundRequest.TransactionId && t.FundId == fund.Id);

            if (transactionDto is null)
            {
                return new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = string.Format(Constants.UnsubscribeFund.TransactionNotFound, unsubscribeFundRequest.TransactionId, fund.Name) };
            }
            if (transactionDto.Type is not TransactionTypeEnum.Opening)
            {
                return new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = string.Format(Constants.UnsubscribeFund.TransactionIsNotOpening, unsubscribeFundRequest.TransactionId) };
            }
            if (transactionDto.Active is not null && !transactionDto.Active.Value)
            {
                return new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = string.Format(Constants.UnsubscribeFund.TransactionAlreadyUnsubscribed, unsubscribeFundRequest.TransactionId) };
            }
            transactionDto.Active = false;
            customer.Balance += transactionDto.Amount;
            var newTransaction = new FundTransactionDto
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Amount = transactionDto.Amount,
                Date = DateTime.Now,
                FundId = fund.Id,
                Type = TransactionTypeEnum.Cancellation
            };
            customer.Transactions.Add(newTransaction);

            var result = await _customerRepository.Update(customer, cancellationToken);

            var response = new TransactionFundResponse();
            if (result is null)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = string.Format(Constants.UnsubscribeFund.Error, fund.Name);
            }
            else
            {
                response.Status = HttpStatusCode.OK;
                response.Message = string.Format(Constants.UnsubscribeFund.Unsubscribed, fund.Name);
                response.TransactionId = newTransaction.Id;
                response.NewBalance = customer.Balance.FormatToColombianCurrency();
            }
            return response;
        }

        public async Task<TransactionsHistoryResponse> GetTransactionHistory(string customerId, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetById(customerId, cancellationToken);
            if (customer is null)
            {
                return new TransactionsHistoryResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.CustomerNotFound };
            }

            List<string> fundIds = customer.Transactions.Select(t => t.FundId).Distinct().ToList();

            var funds = await _fundRepository.GetManyById(fundIds, cancellationToken);
            if (funds.Count == 0)
            {
                return new TransactionsHistoryResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.FundsNotFound };
            }

            TransactionsHistoryResponse transactionsHistoryResponse = new();
            transactionsHistoryResponse.Transactions = _mapper.Map<List<TransactionItem>>(customer.Transactions);
            FormatTransactions(transactionsHistoryResponse.Transactions, fundIds, funds);
            transactionsHistoryResponse.Status = HttpStatusCode.OK;
            transactionsHistoryResponse.Message = Constants.TransactionsFund.TransactionsFound;
            transactionsHistoryResponse.TransactionsCount = transactionsHistoryResponse.Transactions.Count;

            return transactionsHistoryResponse; 
        }

        public async Task<TransactionsDetailsResponse> GetTransactionsDetails(string customerId, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetById(customerId, cancellationToken);
            if (customer is null)
            {
                return new TransactionsDetailsResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.CustomerNotFound };
            }

            var funds = await _fundRepository.GetAll(cancellationToken);
            if (funds.Count == 0)
            {
                return new TransactionsDetailsResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.FundsNotFound };
            }

            TransactionsDetailsResponse transactionsDetailsResponse = new();
            transactionsDetailsResponse.AvailableFunds = funds;

            transactionsDetailsResponse.ActiveTransactions = _mapper.Map<List<TransactionItem>>(customer.Transactions.Where(t=>t.Type == TransactionTypeEnum.Opening && t.Active is not null && t.Active.Value).ToList() ?? []);

            var fundIds = transactionsDetailsResponse.ActiveTransactions.Select(t => t.FundId).Distinct().ToList();
            
            FormatTransactions(transactionsDetailsResponse.ActiveTransactions, fundIds, funds);
            transactionsDetailsResponse.Status = HttpStatusCode.OK;
            transactionsDetailsResponse.Message = Constants.TransactionsFund.DetailTransactionsFound;

            transactionsDetailsResponse.Balance = customer.Balance.FormatToColombianCurrency();

            double amountInvested = transactionsDetailsResponse.ActiveTransactions.Sum(t=>t.Amount);

            transactionsDetailsResponse.AmountInvested = amountInvested.FormatToColombianCurrency();

            return transactionsDetailsResponse; 
        }

        private async Task<(TransactionFundResponse?, CustomerDto, FundDto)> VerifyCustomerAndFund(string CustomerId, string FundId, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetById(CustomerId, cancellationToken);
            if (customer is null)
            {
                return (new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.CustomerNotFound }, new CustomerDto(), new FundDto());
            }

            var fund = await _fundRepository.GetById(FundId, cancellationToken);
            if (fund is null)
            {
                return (new TransactionFundResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.FundNotFound }, new CustomerDto(), new FundDto());
            }

            return (null, customer, fund);
        }

        private void FormatTransactions(List<TransactionItem> transactionItems, List<string> fundIds, List<FundDto> fundDtos)
        {
            foreach (var id in fundIds)
            {
                var fund = fundDtos.Where(d => d.Id == id).FirstOrDefault();
                if (fund != null)
                {
                    transactionItems!.Where(t => t.FundId == id)
                         .ToList()
                         .ForEach(t => t.FundName = fund.Name);
                }
            }
        }

        public async Task<long> CreateInitialFunds(CancellationToken cancellationToken = default)
        {
            return await _fundRepository.CreateInitialFunds(cancellationToken);
        }

        public async Task<long> VerifyFunds(CancellationToken cancellationToken = default)
        {
            return await _fundRepository.VerifyFunds(cancellationToken);
        }
    }
}
