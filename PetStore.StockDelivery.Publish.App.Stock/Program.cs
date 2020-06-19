using PetStore.Domain.Models;
using PetStore.Shared;
using PetStore.Shared.RabbitMQ;
using PetStore.StockDelivery.Client.Client;
using System;

namespace PetStore.StockDelivery.Publish.App.Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stockItemA = new StockItem() { Name = "Item A1" };
                var stockItemB = new StockItem() { Name = "Item B1" };
                var stockItemC = new StockItem() { Name = "Item C1" };

                Console.WriteLine("Add stock");
                var publisher = new StockDeliveryClient(new RabbitMQConfig("localhost", "guest", "guest"));
                publisher.Send(stockItemA);
                publisher.Send(stockItemB);
                publisher.Send(stockItemC);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex);
            }
        }
    }
}
