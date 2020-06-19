using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace PetStore.Shared.RabbitMQ
{
    public abstract class BaseSendReceiveClient
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly string _requestQueueName;
        private readonly string _responseQueueName;
        private readonly string _exchangeName;

        public BaseSendReceiveClient(RabbitMQConfig rabbitMQConfig, string requestQueueName, string responseQueueName, string exchangeName)
        {
            if (rabbitMQConfig is null)
            {
                throw new ArgumentNullException(nameof(rabbitMQConfig));
            }

            if (string.IsNullOrEmpty(requestQueueName))
            {
                throw new ArgumentException("message", nameof(requestQueueName));
            }

            if (string.IsNullOrEmpty(responseQueueName))
            {
                throw new ArgumentException("message", nameof(responseQueueName));
            }

            _factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password
            };
            _requestQueueName = requestQueueName;
            _responseQueueName = responseQueueName;
            _exchangeName = exchangeName;

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(requestQueueName, true, false, false, null);
            _channel.QueueDeclare(responseQueueName, true, false, false, null);
            _consumer = new EventingBasicConsumer(this._channel);
            _consumer.Received += Receive;
            _channel.BasicConsume(_responseQueueName, true, _consumer);
        }

        protected abstract void Receive(object sender, BasicDeliverEventArgs e);

        protected void Publish(byte[] message, string correlationId)
        {
            var props = this._channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = _responseQueueName;
            _channel.BasicPublish(_exchangeName, _requestQueueName, props, message);
        }
    }
}