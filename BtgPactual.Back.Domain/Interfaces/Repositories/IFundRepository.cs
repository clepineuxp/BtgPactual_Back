using BtgPactual.Back.Domain.Dtos.Funds;

namespace BtgPactual.Back.Domain.Interfaces.Repositories
{
    public interface IFundRepository
    {
        Task<FundDto?> GetById(string id, CancellationToken cancellationToken = default);
        Task<List<FundDto>> GetManyById(List<string> ids, CancellationToken cancellationToken = default);
        Task<List<FundDto>> GetAll(CancellationToken cancellationToken = default);
        Task<long> CreateInitialFunds(CancellationToken cancellationToken = default);
        Task<long> VerifyFunds(CancellationToken cancellationToken = default);
    }
}
