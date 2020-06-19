using RabbitMQ.Client;

namespace PetStore.Shared.RabbitMQ
{
    public class BaseSendClient
    {
        protected ConnectionFactory factory;

        public BaseSendClient(RabbitMQConfig rabbitMQConfig)
        {
            factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                DispatchConsumersAsync = true
            };
        }
    }
}
