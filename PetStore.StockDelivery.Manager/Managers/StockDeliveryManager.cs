﻿using PetStore.Data.Repositorys.Interface;
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

        public async Task Create(StockItem stockItem)
        {
            await _stockItemRepository.Create(stockItem);
        }
    }
}