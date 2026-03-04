using EnergiApp.Domain.Repositories;

using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;


namespace EnergiApp.Infrastructure.Persistence.Repositories;
    
public class TradeRepository : ITradeRepository
{
        private readonly AppDbContext _db;
        public TradeRepository(AppDbContext db) => _db = db;
        public Task<TradeEntity?> GetAsync(Guid id) => _db.Trades.Include(a => a.Trades).FirstOrDefaultAsync(a => a.Id == id);
        public Task<List<TradeEntity>> GetAllAsync() => _db.Trades.ToListAsync(); 
        public async Task AddAsync(TradeEntity trade) => await _db.Trades.AddAsync(trade); 
        public Task SaveAsync() => _db.SaveChangesAsync();
}

