using Microsoft.EntityFrameworkCore;
using PetStore.Data.Context;
using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using Polly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Data.Repositorys
{
    public class StockItemRepository : DataManager<StockItem, PetStoreContext>, IStockItemRepository
    {
        public StockItemRepository(PetStoreContext context, IAsyncPolicy retryPolicy)
           : base(context, retryPolicy)
        {
        }

        public async Task<StockItem> GetByName(string name)
        {
            var result = new StockItem();
            await _retryPolicy.ExecuteAsync(async () =>
            {
                result = await context.StockItems.FirstOrDefaultAsync(x => x.Name == name);
            });
            return result;
        }

        public void UpdateDontSave(StockItem stockItem)
        {
            context.StockItems.Update(stockItem);
        }

        public async Task SaveChangesAsync()
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                await context.SaveChangesAsync();
            });
        }

        public async Task<List<StockItem>> GetAll()
        {
            var result = new List<StockItem>();
            await _retryPolicy.ExecuteAsync(async () =>
            {
                result = await context.StockItems.OrderBy(x => x.Name).ToListAsync();
            });
            return result;
        }
    }
}