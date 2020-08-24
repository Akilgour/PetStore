using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using PetStore.StockDelivery.Manager.Managers.Interface;
using System.Threading.Tasks;

namespace PetStore.StockDelivery.Manager.Managers
{
    public class StockDeliveryManager : IStockDeliveryManager
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockDeliveryManager(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task CreateOrUpdate(StockItem stockItem)
        {
            var stockItemFromRepository = await _stockItemRepository.GetByName(stockItem.Name);

            if (stockItemFromRepository == null)
            {
                await _stockItemRepository.Insert(stockItem);
            }
            else
            {
                stockItemFromRepository.Quantity += stockItem.Quantity;
                await _stockItemRepository.Update(stockItemFromRepository);
            }
        }
    }
}