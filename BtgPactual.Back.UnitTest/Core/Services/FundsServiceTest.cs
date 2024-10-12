using BtgPactual.Back.Api.Controllers;
using BtgPactual.Back.Core.Services;
using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Interfaces.Repositories;
using BtgPactual.Back.Domain.Interfaces.Services;
using BtgPactual.Back.UnitTest.MockedResponse;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Misc;
using Moq;
using System.Net;
using Xunit;

namespace BtgPactual.Back.UnitTest.Core.Services
{
    public class FundsServiceTest : BaseTestService<FundsService>
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<IFundRepository> _fundRepositoryMock = new();
        private readonly FundsController _fundsController;
        private readonly FundsService _fundsService;
        public FundsServiceTest()
        {
            _fundsService = new(_customerRepositoryMock.Object,_fundRepositoryMock.Object,mapper);
            _fundsController = new FundsController(_fundsService);
        }

        [Fact]
        public async Task SubscribeFundSuccess()
        {
            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDto()));
            _customerRepositoryMock.Setup(x => x.Update(It.IsAny<CustomerDto>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDto()));
            _fundRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateFund()));

            using (new AssertionScope())
            {
                var response = await _fundsController.SubscribeFund(MockedResponseGenerator.GenerateSubscribeFundRequestSuccess(), It.IsAny<CancellationToken>());
                OkObjectResult actionResponse = response as OkObjectResult;
                TransactionFundResponse result = (TransactionFundResponse)actionResponse?.Value;

                Assert.NotNull(result);
                Assert.True(result.Status == HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task SubscribeFundNotBalance()
        {
            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDtoWithoutBalance()));
            _customerRepositoryMock.Setup(x => x.Update(It.IsAny<CustomerDto>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDtoWithoutBalance()));
            _fundRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateFund()));

            using (new AssertionScope())
            {
                var response = await _fundsController.SubscribeFund(MockedResponseGenerator.GenerateSubscribeFundRequestSuccess(), It.IsAny<CancellationToken>());
                BadRequestObjectResult actionResponse = response as BadRequestObjectResult;
                TransactionFundResponse result = (TransactionFundResponse)actionResponse?.Value;

                Assert.NotNull(result);
                Assert.True(result.Status == HttpStatusCode.BadRequest);
                Assert.Contains("No tiene saldo disponible", result.Message);
            }
        }

        [Fact]
        public async Task UnsubscribeFundSuccess()
        {
            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDto()));
            _customerRepositoryMock.Setup(x => x.Update(It.IsAny<CustomerDto>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDto()));
            _fundRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateFund()));

            using (new AssertionScope())
            {
                var response = await _fundsController.UnsubscribeFund(MockedResponseGenerator.GenerateUnsubscribeFundRequestSuccess("123","6707a54b287557cd1d5c2c4a"), It.IsAny<CancellationToken>());
                OkObjectResult actionResponse = response as OkObjectResult;
                TransactionFundResponse result = (TransactionFundResponse)actionResponse?.Value;

                Assert.NotNull(result);
                Assert.True(result.Status == HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task GetTransactionHistorySuccess()
        {
            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDto()));
            _fundRepositoryMock.Setup(x => x.GetManyById(It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateListFunds()));

            using (new AssertionScope())
            {
                var response = await _fundsController.GetTransactionHistory(MockedResponseGenerator.GenerateUserId(), It.IsAny<CancellationToken>());
                OkObjectResult actionResponse = response as OkObjectResult;
                TransactionsHistoryResponse result = (TransactionsHistoryResponse)actionResponse?.Value;

                Assert.NotNull(result);
                Assert.True(result.Status == HttpStatusCode.OK);
                Assert.NotNull(result.Transactions);
                Assert.True(result.Transactions.Count > 0);
            }
        }

        [Fact]
        public async Task GetTransactionsDetailsSuccess()
        {
            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateCustomerDto()));
            _fundRepositoryMock.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(MockedResponseGenerator.GenerateListFunds()));

            using (new AssertionScope())
            {
                var response = await _fundsController.GetTransactionsDetails(MockedResponseGenerator.GenerateUserId(), It.IsAny<CancellationToken>());
                OkObjectResult actionResponse = response as OkObjectResult;
                TransactionsDetailsResponse result = (TransactionsDetailsResponse)actionResponse?.Value;

                Assert.NotNull(result);
                Assert.True(result.Status == HttpStatusCode.OK);
                Assert.NotNull(result.ActiveTransactions);
                Assert.NotNull(result.AvailableFunds);
                Assert.True(result.ActiveTransactions.Count > 0);
                Assert.True(result.AvailableFunds.Count > 0);
            }
        }
    }
}
