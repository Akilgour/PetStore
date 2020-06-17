using PetStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client.App
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var message = "PetStore OrderItem";
            Console.Title = message;
            Console.WriteLine(message);

            var stockOrder = new StockOrder()
            {
                OrderNumber = "ABC 123",
                OrderItems = new List<StockItem>()            
                {
                    new StockItem() { Name = "Item A", Quantity = 12 },
                    new StockItem() { Name = "Item B", Quantity = 13 },
                    new StockItem() { Name = "Item C", Quantity = 14 }
                }
            };

            var orderItemClient = new OrderItemClient();
            var responce = await orderItemClient.Publish(stockOrder);

            Console.WriteLine(responce.Success);
            Console.WriteLine(responce.Message);

            Console.ReadKey();
        }
    }
}