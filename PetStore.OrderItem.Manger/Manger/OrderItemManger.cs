using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using PetStore.Shared.QueMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Manger.Manger
{
    public class OrderItemManger
    {
        private readonly IStockItemRepository _stockItemRepository;

        public OrderItemManger(IStockItemRepository  stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<OrderResponse> Order(StockOrder stockOrder)
        {
            throw new NotImplementedException();
        }
    }
}
