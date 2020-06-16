using PetStore.Shared;
using RabbitMQ.Client;

namespace PetStore.StockDelivery.Client.Client
{
    public class BaseClient
    {
        protected ConnectionFactory factory;

        public BaseClient(RabbitMQConfig rabbitMQConfig)
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
