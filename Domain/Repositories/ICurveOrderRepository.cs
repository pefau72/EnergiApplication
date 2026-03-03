using EnergiApp.Domain;

namespace EnergiApp.Domain.Repositories;
    public interface ICurveOrderRepository
    {
        Task<CurveOrder?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<CurveOrder>> GetOpenAsync();
        Task<IEnumerable<CurveOrder>> GetAllAsync();
        Task AddAsync(CurveOrder curveorder);
        Task UpdateAsync(CurveOrder curveorder);
        Task SaveChangesAsync();
    }

    
    
    
    