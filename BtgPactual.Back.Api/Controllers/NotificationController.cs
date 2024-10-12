using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Dtos.Response;
using BtgPactual.Back.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BtgPactual.Back.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("send-email/{transactionId}/{customerId}/{email}")]
        [SwaggerOperation(Summary = "It allows send notification email")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(GenericResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> SendEmail(string transactionId, string email, string customerId, CancellationToken cancellationToken = default)
        {
            GenericResponse result = await _notificationService.SendNotificationEmail(transactionId, email, customerId, cancellationToken);

            return result.Status == HttpStatusCode.OK ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("send-sms/{transactionId}/{customerId}/{sms}")]
        [SwaggerOperation(Summary = "It allows send notification sms")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(GenericResponse))]
        [Produces("application/json")]
        public async Task<IActionResult> SendSms(string transactionId, string sms, string customerId, CancellationToken cancellationToken = default)
        {
            GenericResponse result = await _notificationService.SendNotificationSms(transactionId, sms, customerId, cancellationToken);

            return result.Status == HttpStatusCode.OK ? Ok(result) : BadRequest(result);
        }
    }
}
