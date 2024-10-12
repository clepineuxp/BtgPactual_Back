namespace BtgPactual.Back.Domain.Interfaces.Notifications
{
    public interface ISmsService
    {
        bool Send(string body, string destination);
    }
}
