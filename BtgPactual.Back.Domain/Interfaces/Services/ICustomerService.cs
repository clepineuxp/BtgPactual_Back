using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Dtos.Customers.Response;

namespace BtgPactual.Back.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<CustomerInfoResponse> GetDefaultCustomer(CancellationToken cancellationToken = default);
    }
}
