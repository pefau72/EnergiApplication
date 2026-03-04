using EnergiApp.Domain;
using EnergiApp.Domain.Repositories;

using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;


namespace EnergiApp.Infrastructure.Persistence.Repositories;
    
public class TradeRepository : ITradeRepository
{
        private readonly AppDbContext _db;

        public TradeRepository(AppDbContext db) => _db = db; // 

        public Task<Trade?> GetByIdAsync(Guid id)
        {
            IQueryable<TradeEntity> query = _db.Trades; // Start with the base query
            
            var entity = query.FirstOrDefaultAsync(a => a.Id == id); // Execute the query to get the entity
            return entity.ContinueWith(t => t.Result != null ? TradeMapping.ToDomain(t.Result) : null); // Map the entity to the domain model if it exists, otherwise return null
    }

        public Task<List<TradeEntity>> GetAllAsync() => _db.Trades.ToListAsync(); 

        public async Task AddAsync(TradeEntity trade) => await _db.Trades.AddAsync(trade); 

        public Task SaveChangesAsync() => _db.SaveChangesAsync();
 
        public async Task AddAsync(Trade trade) // Method to add a new auction to the database
        {
            var entity = TradeMapping.ToEntity(trade); // Map the domain model to an entity
            await _db.Trades.AddAsync(entity); // Add the entity to the Auctions DbSet
        }

        public Task UpdateAsync(Trade trade) // Method to update an existing auction in the database
        {
            var entity = TradeMapping.ToEntity(trade); // Map the domain model to an entity
            _db.Trades.Update(entity); // Update the entity in the Auctions DbSet
            return Task.CompletedTask; // Return a completed task since the update operation is synchronous
        }

 


}