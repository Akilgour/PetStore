using PetStore.API.Service.Service.Interface;
using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service
{
    public class StockManagerService : IStockManagerService
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockManagerService(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task Delete(Guid id)
        {
            await _stockItemRepository.Delete(id);
        }

        public async Task<List<StockItem>> GetAll()
        {
            return await _stockItemRepository.GetAll();
        }

        public async Task<StockItem> GetById(Guid id)
        {
            return await _stockItemRepository.GetById(id);
        }

        public async Task Update(StockItem stockItem)
        {
            await _stockItemRepository.Update(stockItem);
        }
    }
}