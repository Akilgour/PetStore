using PetStore.Domain.Models;
using PetStore.OrderMonkey.Factorys;
using PetStore.Shared.Helpers;
using PetStore.StockDelivery.Manager.Managers.Interface;
using System;
using System.Threading.Tasks;

namespace PetStore.OrderMonkey
{
    public class Application
    {
        private readonly IStockDeliveryManager _stockDeliveryManager;
        private const int minValue = 1;
        private const int maxValue = 36;

        private const int maxQuantityValue = 20;

        public Application(IStockDeliveryManager stockDeliveryManager)
        {
            _stockDeliveryManager = stockDeliveryManager;
        }

        public async Task Run()
        {
            using (var colour = new ScopedConsoleColourHelper(ConsoleColor.Yellow))
            {
                Console.WriteLine("Everybody’s Got Something to Hide Except Me and My Monkey");
            }

            await _stockDeliveryManager.CreateOrUpdate(new StockItem() { Name = "Mr Marvelous Monkey Munch", Quantity = 100 });

            var stockList = StockList.Create();

            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random();
                int index = rnd.Next(minValue, maxValue);
                int quantity = rnd.Next(minValue, maxQuantityValue);

                var stockItem = stockList[index];

                using (var colour = new ScopedConsoleColourHelper())
                {
                    Console.WriteLine($" Name { stockItem.Name} Quantity {quantity}");
                }

                await _stockDeliveryManager.CreateOrUpdate(new StockItem() { Name = stockItem.Name, Quantity = quantity });
                await Task.Delay(2000);
            }
        }
    }
}