using PetStore.Shared;
using RabbitMQ.Client;

namespace PetStore.StockDelivery.Publish.Publisher
{
    public abstract class BasePublisher
    {
        protected ConnectionFactory factory;

        public BasePublisher(RabbitMQConfig rabbitMQConfig)
        {
            factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password
            };
        }
    }
}