using AutoMapper;
using BtgPactual.Back.Domain.Configurations;
using BtgPactual.Back.Domain.Constants;
using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Dtos.Funds;
using BtgPactual.Back.Domain.Enums;
using BtgPactual.Back.Domain.Interfaces.Repositories;
using BtgPactual.Back.Domain.Models.Customers;
using BtgPactual.Back.Domain.Models.Funds;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BtgPactual.Back.Infrastructure.DataAccess.Repositories
{
    public class FundRepository : IFundRepository
    {
        internal readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<Fund> _collection;
        private readonly IMapper _mapper;
        private readonly ILogger<FundRepository> _logger;

        public FundRepository(DatabaseConfiguration databaseConfiguration, IMapper mapper, ILogger<FundRepository> logger)
        {
            _dbContext = new MongoDbContext(databaseConfiguration);
            _collection = _dbContext.db.GetCollection<Fund>(Constants.Collections.Funds);
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<long> CreateInitialFunds(CancellationToken cancellationToken = default)
        {
            try
            {
                DateTime date = DateTime.Now;
                List<Fund> funds = new List<Fund>()
                {
                    new Fund{ Name = "FPV_BTG_PACTUAL_RECAUDADORA", MinimumAmount = 75000, Category = FundCategoryEnum.FPV, CreateAt = date, UpdateAt = date },
                    new Fund{ Name = "FPV_BTG_PACTUAL_ECOPETROL", MinimumAmount = 125000, Category = FundCategoryEnum.FPV, CreateAt = date, UpdateAt = date },
                    new Fund{ Name = "DEUDAPRIVADA", MinimumAmount = 50000, Category = FundCategoryEnum.FIC, CreateAt = date, UpdateAt = date },
                    new Fund{ Name = "FDO-ACCIONES", MinimumAmount = 250000, Category = FundCategoryEnum.FIC, CreateAt = date, UpdateAt = date },
                    new Fund{ Name = "FPV_BTG_PACTUAL_DINAMICA ", MinimumAmount = 100000, Category = FundCategoryEnum.FPV, CreateAt = date, UpdateAt = date },
                };
                foreach (Fund fund in funds)
                {
                    await _collection.InsertOneAsync(fund, cancellationToken: cancellationToken);
                }

                return funds.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurs during create initial funds db in FundRepository.CreateInitialFunds");
                return 0;
            }
        }

        public async Task<FundDto?> GetById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _collection.Find(c => c.Id == new ObjectId(id)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return result == null ? null : _mapper.Map<FundDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retriving fund {id} from db in FundRepository.GetById", id);
                return null;
            }
        }

        public async Task<List<FundDto>> GetManyById(List<string> ids, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _collection.Find(c => ids.Contains(c.Id.ToString())).ToListAsync(cancellationToken: cancellationToken);

                return result == null ? [] : _mapper.Map<List<FundDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retriving funds from db in FundRepository.GetManyById");
                return [];
            }
        }

        public async Task<List<FundDto>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _collection.Find(new BsonDocument()).ToListAsync(cancellationToken: cancellationToken);

                return result == null ? [] : _mapper.Map<List<FundDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retriving funds from db in FundRepository.GetManyById");
                return [];
            }
        }

        public async Task<long> VerifyFunds(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _collection.Find(new BsonDocument()).CountDocumentsAsync(cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurs during verify funds db in FundRepository.VerifyFunds");
                return 0;
            }
        }
    }
}
