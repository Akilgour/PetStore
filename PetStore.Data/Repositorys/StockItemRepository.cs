using Microsoft.EntityFrameworkCore;
using PetStore.Data.Context;
using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using Polly;
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
    }
}