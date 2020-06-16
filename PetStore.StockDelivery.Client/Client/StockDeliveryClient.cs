using PetStore.Domain.Models;
using PetStore.Shared;
using PetStore.Shared.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.StockDelivery.Client.Client
{
    public class StockDeliveryClient : BaseClient
    {
        public StockDeliveryClient(RabbitMQConfig rabbitMQConfig)
            : base(rabbitMQConfig)
        {
        }

        public void Receive( )
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
                    var stockItem = (StockItem)body.DeSerialize(typeof(StockItem));
                    NewMethod(stockItem);
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