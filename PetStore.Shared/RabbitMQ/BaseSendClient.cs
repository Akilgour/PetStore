using RabbitMQ.Client;

namespace PetStore.Shared.RabbitMQ
{
    public class BaseSendClient
    {
        private readonly string _requestQueueName;
        private readonly string _exchangeName;
        private readonly ConnectionFactory _factory;

        public BaseSendClient(RabbitMQConfig rabbitMQConfig, string requestQueueName, string exchangeName)
        {
            if (rabbitMQConfig is null)
            {
                throw new System.ArgumentNullException(nameof(rabbitMQConfig));
            }

            if (string.IsNullOrEmpty(requestQueueName))
            {
                throw new System.ArgumentException("message", nameof(requestQueueName));
            }

            _factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password
            };
            _requestQueueName = requestQueueName;
            _exchangeName = exchangeName;
        }

        protected void Send(byte[] item)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _requestQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicPublish(_exchangeName, _requestQueueName, null, item);
        }
    }
}