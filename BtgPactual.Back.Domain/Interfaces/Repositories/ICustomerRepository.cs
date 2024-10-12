using BtgPactual.Back.Domain.Dtos.Customers;

namespace BtgPactual.Back.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerDto?> GetById(string id, CancellationToken cancellationToken = default);
        Task<CustomerDto?> GetDefaultCustomer(CancellationToken cancellationToken = default);
        Task<CustomerDto?> Update(CustomerDto customerDto, CancellationToken cancellationToken = default);
    }
}
