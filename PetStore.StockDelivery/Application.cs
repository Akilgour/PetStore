using PetStore.Domain.Models;
using PetStore.Shared.Helpers;
using PetStore.Shared.RabbitMQ;
using PetStore.Shared.RabbitMQ.Factorys;
using PetStore.StockDelivery.Manager.Managers.Interface;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace PetStore.StockDelivery
{
    public class Application : BaseReceivedServer
    {
        private readonly IStockDeliveryManager _stockDeliveryManager;
        private const string _requestQueueName = "StockDelivery_Que";
        public Application(IStockDeliveryManager stockDeliveryManager)
            : base(RabbitMQConfigFactory.Create(), _requestQueueName)
        {
            _stockDeliveryManager = stockDeliveryManager;
        }

        public void Run()
        {
            WaitForResponse();
        }

        protected override async Task Received(object sender, BasicDeliverEventArgs @event)
        {
            var stockItem = (StockItem)@event.Body.ToArray().DeSerialize(typeof(StockItem));

            using (var colour = new ScopedConsoleColourHelper())
            {
                Console.WriteLine($"Name: {stockItem.Name} Quantity: {stockItem.Quantity}");
            }

            await _stockDeliveryManager.Create(stockItem);
        }
    }
}