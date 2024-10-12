using AutoMapper;
using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Interfaces.Repositories;
using BtgPactual.Back.Domain.Interfaces.Services;

namespace BtgPactual.Back.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerInfoResponse> GetDefaultCustomer(CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetDefaultCustomer(cancellationToken) ?? throw new Exception("Can not retrive Default Customer");
            
            return _mapper.Map<CustomerInfoResponse>(customer);

        }
    }
}
