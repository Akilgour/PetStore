using Microsoft.EntityFrameworkCore;
using PetStore.Domain.Models;

namespace PetStore.Data.Context
{
    public class PetStoreContext : BaseContext
    {
        public PetStoreContext(DbContextOptions<PetStoreContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public PetStoreContext()
            :base()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<StockItem> StockItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = PetStore");
            }
        }
    }
}