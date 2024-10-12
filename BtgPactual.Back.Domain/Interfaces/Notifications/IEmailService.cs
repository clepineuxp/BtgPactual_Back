namespace BtgPactual.Back.Domain.Interfaces.Notifications
{
    public interface IEmailService
    {
        bool Send(string body, string subject, string destination);
    }
}
