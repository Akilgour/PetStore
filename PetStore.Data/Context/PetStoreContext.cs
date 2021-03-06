﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetStore.Domain.Models;

namespace PetStore.Data.Context
{
    public class PetStoreContext : DbContext
    {

        public PetStoreContext()
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