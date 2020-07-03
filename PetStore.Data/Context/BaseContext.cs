using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Data.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext()
        {
        }

        protected BaseContext(DbContextOptions<PetStoreContext> options)
            : base(options)
        {
        }

        public async Task SaveChangesAsync()
        {
            ChangeTracker.DetectChanges();
            //Setting a var so the date time is the same for create and update on brand new items
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Property("LastModifiedDate").CurrentValue = now;
            }

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                entry.Property("CreatedDate").CurrentValue = now;
            }

            await base.SaveChangesAsync();
        }
    }
}