using BtgPactual.Back.Domain.Dtos.Response;
using BtgPactual.Back.Domain.Interfaces.Notifications;
using BtgPactual.Back.Domain.Interfaces.Repositories;
using BtgPactual.Back.Domain.Interfaces.Services;
using System.Net;
using BtgPactual.Back.Domain.Constants;
using BtgPactual.Back.Core.Helpers;

namespace BtgPactual.Back.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IFundRepository _fundRepository;

        public NotificationService(ISmsService smsService, 
            IEmailService emailService, 
            ICustomerRepository customerRepository,
            IFundRepository fundRepository)
        {
            _smsService = smsService;
            _emailService = emailService;
            _customerRepository = customerRepository;
            _fundRepository = fundRepository;
        }

        public async Task<GenericResponse> SendNotificationEmail(string transactionId, string email, string customerId, CancellationToken cancellationToken = default)
        {
            (var response, var body) = await GetEmailNotification(customerId, transactionId, cancellationToken);

            if (response is not null)
            {
                return response;
            }

            if (_emailService.Send(body, Constants.Notifications.Subject, email))
            {
                return new GenericResponse { Status = HttpStatusCode.OK, Message = Constants.Notifications.NotificationSent };
            }
            return new GenericResponse { Status = HttpStatusCode.BadRequest, Message = Constants.Notifications.NotificationNotSent };
        }

        public async Task<GenericResponse> SendNotificationSms(string transactionId, string sms, string customerId, CancellationToken cancellationToken = default)
        {
            (var response, var body) = await GetEmailNotification(customerId, transactionId, cancellationToken);

            if (response is not null)
            {
                return response;
            }
            if (sms.Length == 10)
            {
                sms = $"+57{sms}";
            }
            if (_smsService.Send(body, sms))
            {
                return new GenericResponse { Status = HttpStatusCode.OK, Message = Constants.Notifications.NotificationSent };
            }
            return new GenericResponse { Status = HttpStatusCode.BadRequest, Message = Constants.Notifications.NotificationNotSent };
        }

        private async Task<(GenericResponse?, string)> GetEmailNotification(string customerId, string transactionId, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetById(customerId, cancellationToken);
            if (customer is null)
            {
                return (new GenericResponse { Status = HttpStatusCode.BadRequest, Message = Constants.TransactionsFund.CustomerNotFound }, string.Empty);
            }

            var transaction = customer.Transactions.FirstOrDefault(t => t.Id == transactionId);

            if (transaction is null)
            {
                return (new GenericResponse { Status = HttpStatusCode.BadRequest, Message = Constants.Notifications.TransactionNotFound }, string.Empty);
            }

            var fund = await _fundRepository.GetById(transaction.FundId, cancellationToken);
            if (fund is null)
            {
                return (new GenericResponse { Status = HttpStatusCode.BadRequest, Message = Constants.Notifications.FundNotFound }, string.Empty);
            }

            string body = string.Format(Constants.Notifications.Template, fund.Name, transaction.Amount.FormatToColombianCurrency(), transaction.Type.GetEnumInfo(), transaction.Date.FormatDateTime());

            return (null, body);
        }
    }
}
