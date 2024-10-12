using BtgPactual.Back.Domain.Dtos.Response;

namespace BtgPactual.Back.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task<GenericResponse> SendNotificationEmail(string transactionId, string email, string customerId, CancellationToken cancellationToken = default);
        Task<GenericResponse> SendNotificationSms(string transactionId, string sms, string customerId, CancellationToken cancellationToken = default);
    }
}
