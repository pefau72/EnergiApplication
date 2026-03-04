using EnergiApp.Domain;
using EnergiApp.Domain.Repositories;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using EnergiApp.Infrastructure.Persistence.Mappers;

namespace EnergiApp.Infrastructure.Persistence.Repositories;


public sealed class AuctionRepository : IAuctionRepository
{
    private readonly AppDbContext _db; 

    public AuctionRepository(AppDbContext db) // Constructor that takes an AppDbContext and assigns it to a private field
    {
        _db = db; // Assign the provided AppDbContext to the private field for use in other methods
    }

    public async Task<Auction?> GetByIdAsync(Guid id, bool includeChildren = false) // Method to get an auction by its ID, with an option to include related entities
    {
        IQueryable<AuctionEntity> query = _db.Auctions; // Start with the base query

        if (includeChildren) // If includeChildren is true, include related entities in the query
        {
            query = query
                .Include(a => a.Contracts)
                .Include(a => a.Currencies)
                .Include(a => a.Portfolios);
        }

        var entity = await query.FirstOrDefaultAsync(a => a.Id == id); // Execute the query to get the entity
        return entity is null ? null : AuctionMapping.ToDomain(entity); // Map to domain model if entity is found, otherwise return null
    }

    public async Task<IReadOnlyList<Auction>> GetOpenAsync() // Method to get all open auctions, including related entities
    {
        var entities = await _db.Auctions // Start with the Auctions DbSet
            .Where(a => a.State == (int)AuctionStateType.Open) // Filter for auctions that are in the Open state
            .Include(a => a.Contracts) // Include related Contracts
            .Include(a => a.Currencies) // Include related Currencies
            .Include(a => a.Portfolios) // Include related Portfolios
            .ToListAsync();

        return entities.Select(AuctionMapping.ToDomain).ToList(); // Map each entity to the domain model and return as a list
    }

    public async Task AddAsync(Auction auction) // Method to add a new auction to the database
    {
        var entity = AuctionMapping.ToEntity(auction); // Map the domain model to an entity
        await _db.Auctions.AddAsync(entity); // Add the entity to the Auctions DbSet
    }

    public Task UpdateAsync(Auction auction) // Method to update an existing auction in the database
    {
        var entity = AuctionMapping.ToEntity(auction); // Map the domain model to an entity
        _db.Auctions.Update(entity); // Update the entity in the Auctions DbSet
        return Task.CompletedTask; // Return a completed task since the update operation is synchronous
    }

    public Task SaveChangesAsync() => _db.SaveChangesAsync(); // Method to save changes to the database, simply calls SaveChangesAsync on the AppDbContext


}



