using PetStore.Domain.Models;
using PetStore.Shared;
using PetStore.StockDelivery.Publish.Publisher;
using System;

namespace PetStore.StockDelivery.Publish.App.Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stockItemA = new StockItem() { Name = "Item A" };
                var stockItemB = new StockItem() { Name = "Item B" };
                var stockItemC = new StockItem() { Name = "Item C" };

                Console.WriteLine("Add stock");
                var publisher = new StockDeliveryPublisher(new RabbitMQConfig("localhost", "guest", "guest"));
                publisher.Publish(stockItemA);
                publisher.Publish(stockItemB);
                publisher.Publish(stockItemC);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex);
            }
        }
    }
}
