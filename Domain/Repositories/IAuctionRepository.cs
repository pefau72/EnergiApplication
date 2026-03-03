using EnergiApp.Domain;
namespace EnergiApp.Domain.Repositories
{
    public interface IAuctionRepository
    {
        Task<Auction?> GetByIdAsync(Guid id, bool includeChildren = false); // Get an auction by its unique identifier, optionally including related entities.
        Task<IReadOnlyList<Auction>> GetOpenAsync(); // Get all auctions that are currently open for bidding.
        Task AddAsync(Auction auction); // Add a new auction to the database.
        Task UpdateAsync(Auction auction); // Update an existing auction in the database.
        Task SaveChangesAsync(); // Save changes to db after adding or updating auctions.
    }
}
