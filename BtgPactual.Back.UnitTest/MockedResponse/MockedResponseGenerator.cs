using BtgPactual.Back.Domain.Constants;
using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Dtos.Customers.Request;
using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Dtos.Funds;
using BtgPactual.Back.Domain.Enums;
using BtgPactual.Back.Domain.Models.Funds;
using MongoDB.Bson;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BtgPactual.Back.UnitTest.MockedResponse
{
    public static class MockedResponseGenerator
    {
        public static string GenerateUserId() => "6707a532287557cd1d5c2c46";


        public static TransactionsDetailsResponse GenerateTransactionsDetailsResponseSuccessfully()
        {
            return new TransactionsDetailsResponse
            {
                ActiveTransactions = [],
                AvailableFunds = [],
                AmountInvested = "$ 250.000,00",
                Balance = "$ 250.000,00",
                Message = Constants.TransactionsFund.TransactionsFound,
                Status = HttpStatusCode.OK

            };
        }

        public static TransactionsHistoryResponse GenerateTransactionsHistoryResponseSuccessfully()
        {
            return new TransactionsHistoryResponse
            {
                Transactions = [],
                TransactionsCount = 0,
                Message = Constants.TransactionsFund.TransactionsFound,
                Status = HttpStatusCode.OK

            };
        }

        public static SubscribeFundRequest GenerateSubscribeFundRequestSuccess()
        {
            return new SubscribeFundRequest
            {
                Amount = 250000,
                CustomerId = GenerateUserId(),
                FundId = GenerateUserId(),
            };
        }

        public static UnsubscribeFundRequest GenerateUnsubscribeFundRequestSuccess(string? transactionId = null, string? fundId = null)
        {
            return new UnsubscribeFundRequest
            {
                CustomerId = GenerateUserId(),
                FundId = fundId ?? ObjectId.GenerateNewId().ToString(),
                TransactionId = transactionId ?? ObjectId.GenerateNewId().ToString()
            };
        }

        public static CustomerDto? GenerateCustomerDto()
        {
            return new CustomerDto
            {
                Balance = 500000,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id = GenerateUserId(),
                Name = "Default",
                Transactions = GenerateListFundTransaction()
            };
        }

        public static CustomerDto? GenerateCustomerDtoWithoutBalance()
        {
            return new CustomerDto
            {
                Balance = 100,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id = GenerateUserId(),
                Name = "Default",
                Transactions = GenerateListFundTransaction()
            };
        }

        public static List<FundTransactionDto> GenerateListFundTransaction()
        {
            return new List<FundTransactionDto>
            {
                new FundTransactionDto { Id = "1234", FundId = "6707a54b287557cd1d5c2c48", Amount = 150000, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Cancellation },
                new FundTransactionDto { Id = "1234", FundId = "6707a54b287557cd1d5c2c4a", Amount = 150000, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Cancellation },
                new FundTransactionDto { Id = "123", FundId = "6707a54b287557cd1d5c2c47", Amount = 150000, Active = true, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Opening },
                new FundTransactionDto { Id = "123", FundId = "6707a54b287557cd1d5c2c49", Amount = 150000, Active = true, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Opening },
                new FundTransactionDto { Id = "123", FundId = "6707a54b287557cd1d5c2c4b", Amount = 150000, Active = true, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Opening },
                new FundTransactionDto { Id = "123", FundId = "6707a54b287557cd1d5c2c48", Amount = 150000, Active = true, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Opening },
                new FundTransactionDto { Id = "123", FundId = "6707a54b287557cd1d5c2c4a", Amount = 150000, Active = true, Date = DateTime.Now.AddDays(-1), Type = TransactionTypeEnum.Opening }

            };
        }

        public static List<FundDto> GenerateListFunds()
        {
            DateTime date = DateTime.Now.AddHours(-25);
            return new List<FundDto>
            {
                new FundDto{ Id = "6707a54b287557cd1d5c2c47", Name = "FPV_BTG_PACTUAL_RECAUDADORA", MinimumAmount = 75000, Category = FundCategoryEnum.FPV, CreateAt = date, UpdateAt = date },
                new FundDto{ Id = "6707a54b287557cd1d5c2c48", Name = "FPV_BTG_PACTUAL_ECOPETROL", MinimumAmount = 125000, Category = FundCategoryEnum.FPV, CreateAt = date, UpdateAt = date },
                new FundDto{ Id = "6707a54b287557cd1d5c2c49", Name = "DEUDAPRIVADA", MinimumAmount = 50000, Category = FundCategoryEnum.FIC, CreateAt = date, UpdateAt = date },
                new FundDto{ Id = "6707a54b287557cd1d5c2c4a", Name = "FDO-ACCIONES", MinimumAmount = 250000, Category = FundCategoryEnum.FIC, CreateAt = date, UpdateAt = date },
                new FundDto{ Id = "6707a54b287557cd1d5c2c4b", Name = "FPV_BTG_PACTUAL_DINAMICA ", MinimumAmount = 100000, Category = FundCategoryEnum.FPV, CreateAt = date, UpdateAt = date }
            };
        }

        public static FundDto? GenerateFund()
        {
            DateTime date = DateTime.Now.AddHours(-25);
            return new FundDto { Id = "6707a54b287557cd1d5c2c4a", Name = "FDO-ACCIONES", MinimumAmount = 250000, Category = FundCategoryEnum.FIC, CreateAt = date, UpdateAt = date };
        }
    }
}
