using EnergiApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.db;
using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Mappers;

namespace EnergiApp.Infrastructure.Persistence.Repositories
{




    public sealed class CurveOrderRepository : ICurveOrderRepository
    {
        private readonly AppDbContext _db;

        public CurveOrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CurveOrder?> GetByIdAsync(Guid id)
        {
            IQueryable<CurveOrderEntity> query = _db.CurveOrders;

            var entity = await query.FirstOrDefaultAsync(a => a.Id == id);
            return entity is null ? null : CurveOrderMapping.ToDomain(entity);
        }

   
        public async Task AddAsync(CurveOrder curveorder)
        {
            var entity = CurveOrderMapping.ToEntity(curveorder);
            await _db.CurveOrders.AddAsync(entity);
        }

        public Task UpdateAsync(CurveOrder curveorder)
        {
            var entity = CurveOrderMapping.ToEntity(curveorder);
            _db.CurveOrders.Update(entity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _db.SaveChangesAsync();

        

   
        }
    }

