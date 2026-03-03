using EnergiApp.Domain;
namespace EnergiApp.Domain.Repositories
{
    
    public interface ITradeRepository
    {
        Task<Trade?> GetAsync(Guid id);
        Task<IReadOnlyList<Trade>> GetOpenAsync();
        Task<IEnumerable<Trade>> GetAllAsync();
        Task AddAsync(Trade trade);
        Task UpdateAsync(Trade trade);
        Task SaveChangesAsync();
    }
}

