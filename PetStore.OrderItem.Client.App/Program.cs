using PetStore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var message = "PetStore OrderItem";
            Console.Title = message;
            Console.WriteLine(message);

            var stockItemA = new StockItem() { Name = "Item A", Quantity = 12 };

            var orderItemClient = new OrderItemClient();
            var responce = await orderItemClient.Publish(stockItemA);

            Console.WriteLine(responce.Success);
            Console.WriteLine(responce.Message);
        }
    }
}
