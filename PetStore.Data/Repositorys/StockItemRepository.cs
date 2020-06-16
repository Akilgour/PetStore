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
    }
}
