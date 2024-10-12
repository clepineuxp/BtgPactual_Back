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
    public class StartupController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IFundsService _fundsService;

        public StartupController(ICustomerService customerService, IFundsService fundsService)
        {
            _customerService = customerService;
            _fundsService = fundsService;
        }

        [HttpGet]
        [Route("get-customer")]
        [SwaggerOperation(Summary = "Get a default customer to use all the services of the solution")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CustomerInfoResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> GetDefaultCustomer(CancellationToken cancellationToken = default)
        {
            return Ok(await _customerService.GetDefaultCustomer(cancellationToken));
        }

        [HttpPost]
        [Route("create-initial-funds")]
        [SwaggerOperation(Summary = "Create initial funds for the solution")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(long))]
        [Produces("application/json")]
        public async Task<IActionResult> CreateInitialFunds(CancellationToken cancellationToken = default)
        {
            return Ok(await _fundsService.CreateInitialFunds(cancellationToken));
        }

        [HttpGet]
        [Route("verify-funds")]
        [SwaggerOperation(Summary = "Verify funds of the solution")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(long))]
        [Produces("application/json")]
        public async Task<IActionResult> VerifyFunds(CancellationToken cancellationToken = default)
        {
            return Ok(await _fundsService.VerifyFunds(cancellationToken));
        }
    }
}
