using PetStore.Domain.Models;
using PetStore.Shared;
using PetStore.Shared.Helpers;
using PetStore.Shared.RabbitMQ;
using PetStore.StockDelivery.Client.Client.Interface;
using PetStore.StockDelivery.Manager.Managers.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

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
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    //   https://gigi.nullneuron.net/gigilabs/asynchronous-rabbitmq-consumers-in-net/

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(queueName, true, consumer);

                    while (true)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var stockItem = (StockItem)@event.Body.ToArray().DeSerialize(typeof(StockItem));
            await _stockDeliveryManager.Create(stockItem);
        }
    }
}