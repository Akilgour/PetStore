using PetStore.Domain.Models;
using PetStore.Shared;
using PetStore.Shared.Helpers;
using PetStore.Shared.RabbitMQ;
using RabbitMQ.Client;

namespace PetStore.StockDelivery.Publish.Publisher
{
    public class StockDeliveryPublisher : BasePublisher
    {
        public StockDeliveryPublisher(RabbitMQConfig rabbitMQConfig)
        : base(rabbitMQConfig)
        {
        }

        public void Publish(StockItem stockItem)
        {
            NewMethod("StockDelivery_Que", stockItem.Serialize());
        }

        private void NewMethod(string queueName, byte[] item)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish("", queueName, null, item);
            }
        }
    }
}