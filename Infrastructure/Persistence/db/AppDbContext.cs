using Microsoft.EntityFrameworkCore;
using EnergiApp.Infrastructure.Persistence.Entities; // This namespace contains the entity classes that represent the database tables.   
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EnergiApp.Infrastructure.Persistence.db;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<AuctionEntity> Auctions => Set<AuctionEntity>();
        public DbSet<TradeEntity> Trades => Set<TradeEntity>();
        public DbSet<CurveOrderEntity> CurveOrders => Set<CurveOrderEntity>();
        public DbSet<CurveOrderPointEntity> CurveOrderPoints => Set<CurveOrderPointEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionEntity>(entity =>
            {
                entity.ToTable("auctions");
                entity.HasKey(x => x.Id);
            });
        
            modelBuilder.Entity<TradeEntity>(entity =>
            {
                entity.ToTable("trades");
                entity.HasKey(x => x.Id);

            });

            modelBuilder.Entity<CurveOrderEntity>(entity =>
            {
                entity.ToTable("curve_orders");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<CurveOrderPointEntity>(entity =>
            {
                entity.ToTable("curve_order_points");
                entity.HasKey(x => x.Id);

            });
        }
    }

