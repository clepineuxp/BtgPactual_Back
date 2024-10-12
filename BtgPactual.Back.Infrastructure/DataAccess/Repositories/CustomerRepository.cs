using AutoMapper;
using BtgPactual.Back.Domain.Configurations;
using BtgPactual.Back.Domain.Constants;
using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Interfaces.Repositories;
using BtgPactual.Back.Domain.Models.Customers;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BtgPactual.Back.Infrastructure.DataAccess.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        internal readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<Customer> _collection;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(DatabaseConfiguration databaseConfiguration, IMapper mapper, ILogger<CustomerRepository> logger)
        {
            _dbContext = new MongoDbContext(databaseConfiguration);
            _collection = _dbContext.db.GetCollection<Customer>(Constants.Collections.Customers);
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerDto?> GetById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _collection.Find(c => c.Id == new ObjectId(id)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return result is null ? null : _mapper.Map<CustomerDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retriving customer {id} from db in CustomerRepository.GetById", id);
                return null;
            }
        }

        public async Task<CustomerDto?> GetDefaultCustomer(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _collection.Find(new BsonDocument()).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (result is null)
                {
                    var newCustomer = new CustomerDto
                    {
                        Balance = 500000,
                        Name = "Default Customer"
                    };
                    var newCustomerResult = await Create(newCustomer, cancellationToken) ?? throw new Exception("Can not create Default Customer");
                    return _mapper.Map<CustomerDto>(newCustomerResult);
                }
                return _mapper.Map<CustomerDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retriving default customer from db in CustomerRepository.GetDefaultCustomer");
                return null;
            }
        }

        public async Task<CustomerDto?> Create(CustomerDto customerDto, CancellationToken cancellationToken = default)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerDto);
                customer.CreateAt = DateTime.Now;
                customer.UpdateAt = DateTime.Now;
                customer.Id = ObjectId.GenerateNewId();
                await _collection.InsertOneAsync(customer, cancellationToken: cancellationToken);

                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur during create customer in CustomerRepository.Create");
                return null;
            }
        }

        public async Task<CustomerDto?> Update(CustomerDto customerDto, CancellationToken cancellationToken = default)
        {
            try
            {
                customerDto.UpdateAt = DateTime.Now;
                var result = await _collection.FindOneAndReplaceAsync(c => c.Id == new ObjectId(customerDto.Id), _mapper.Map<Customer>(customerDto), cancellationToken: cancellationToken);
                return result is null? null: _mapper.Map<CustomerDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occur during update customer {id} in CustomerRepository.Update", customerDto.Id);
                return null;
            }
        }
    }
}
