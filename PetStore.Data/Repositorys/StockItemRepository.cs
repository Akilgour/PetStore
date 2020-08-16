using Microsoft.EntityFrameworkCore;
using PetStore.Data.Context;
using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Data.Repositorys
{
    public class StockItemRepository : BaseRepository, IStockItemRepository
    {
        public StockItemRepository(PetStoreContext context, IAsyncPolicy retryPolicy)
           : base(context, retryPolicy)
        {
        }

        public async Task Create(StockItem stockItem)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var addedEntity = _context.Add(stockItem);
                await _context.SaveChangesAsync();
            });
        }

        public async Task<StockItem> GetByName(string name)
        {
            var result = new StockItem();
            await _retryPolicy.ExecuteAsync(async () =>
            {
                result = await _context.StockItems.FirstOrDefaultAsync(x => x.Name == name);
            });
            return result;
        }

        public async Task Update(StockItem stockItem)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                _context.StockItems.Update(stockItem);
                await _context.SaveChangesAsync();
            });
        }

        public void UpdateDontSave(StockItem stockItem)
        {
            _context.StockItems.Update(stockItem);
        }

        public async Task SaveChangesAsync()
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                await _context.SaveChangesAsync();
            });
        }

        public async Task<List<StockItem>> GetAll()
        {
            var result = new List<StockItem>();
            await _retryPolicy.ExecuteAsync(async () =>
            {
                result = await _context.StockItems.OrderBy(x => x.Name).ToListAsync();
            });
            return result;
        }

        public async Task<StockItem> GetById(Guid id)
        {
            var result = new StockItem();
            await _retryPolicy.ExecuteAsync(async () =>
            {
                result = await _context.StockItems.FirstOrDefaultAsync(x => x.Id == id);
            });
            return result;
        }

        public async Task Delete(Guid id)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var item = await _context.StockItems.FirstAsync(x => x.Id == id);
                _context.StockItems.Remove(item);
                await _context.SaveChangesAsync();
            });
        }
    }
}