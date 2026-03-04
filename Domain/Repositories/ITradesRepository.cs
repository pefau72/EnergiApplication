using EnergiApp.Domain;
namespace EnergiApp.Domain.Repositories
{
    
    public interface ITradeRepository
    {
        Task<Trade?> GetByIdAsync(Guid id);
        Task AddAsync(Trade trade);
        Task UpdateAsync(Trade trade);
        Task SaveChangesAsync();
    }
}



