using PetStore.Domain.Models;
using PetStore.Shared;
using PetStore.Shared.Helpers;
using PetStore.StockDelivery.Client.Client.Interface;
using PetStore.StockDelivery.Manager.Managers.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace PetStore.StockDelivery.Client.Client
{
    public class StockDeliveryClient : BaseClient, IStockDeliveryClient
    {
        private readonly IStockDeliveryManager _stockDeliveryManager;

        public StockDeliveryClient(RabbitMQConfig rabbitMQConfig, IStockDeliveryManager stockDeliveryManager)
            : base(rabbitMQConfig)
        {
            _stockDeliveryManager = stockDeliveryManager;
        }

        public void Receive()
        {
            var queueName = "StockDelivery_Que";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    var stockItem = (StockItem)body.DeSerialize(typeof(StockItem));
                    //  NewMethod(stockItem);
                    _stockDeliveryManager.Create(stockItem);

                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }

        private static void NewMethod(StockItem stockItem)
        {
            Console.WriteLine($" id {stockItem.Id}  Name {stockItem.Name}");
        }
    }
}