using BtgPactual.Back.Domain.Dtos.Customers.Request;
using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BtgPactual.Back.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FundsController : ControllerBase
    {
        private readonly IFundsService _fundsService;

        public FundsController(IFundsService fundsService)
        {
            _fundsService = fundsService;
        }

        [HttpPost]
        [Route("subscribe-fund")]
        [SwaggerOperation(Summary = "It allows subscribe customer to fund")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionFundResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(TransactionFundResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> SubscribeFund([FromBody] SubscribeFundRequest subscribeFundRequest, CancellationToken cancellationToken = default)
        {
            TransactionFundResponse result = await _fundsService.SubscribeFund(subscribeFundRequest, cancellationToken);
            
            return result.Status == HttpStatusCode.OK ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("unsubscribe-fund")]
        [SwaggerOperation(Summary = "It allows unsubscribe customer to fund")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionFundResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(TransactionFundResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> UnsubscribeFund([FromBody] UnsubscribeFundRequest unsubscribeFundRequest, CancellationToken cancellationToken = default)
        {
            TransactionFundResponse result = await _fundsService.UnsubscribeFund(unsubscribeFundRequest, cancellationToken);

            return result.Status == HttpStatusCode.OK ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("get-transaction-history/{customerId}")]
        [SwaggerOperation(Summary = "It Allows getting all transactions related to an customer")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionsHistoryResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionsHistoryResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> GetTransactionHistory(string customerId, CancellationToken cancellationToken = default)
        {
            var result = await _fundsService.GetTransactionHistory(customerId, cancellationToken);

            return result.Status == HttpStatusCode.OK ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("get-transactions-details/{customerId}")]
        [SwaggerOperation(Summary = "It Allows getting all details transactions related to an customer")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionsDetailsResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionsDetailsResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> GetTransactionsDetails(string customerId, CancellationToken cancellationToken = default)
        {
            var result = await _fundsService.GetTransactionsDetails(customerId, cancellationToken);

            return result.Status == HttpStatusCode.OK ? Ok(result) : BadRequest(result);
        }
    }
}
