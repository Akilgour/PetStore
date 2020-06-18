using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Shared.RabbitMQ
{
    public abstract class BaseClient
    {
        private readonly string _requestQueueName;
        private readonly ConnectionFactory _factory;
        protected static IModel _channel;

        public BaseClient(RabbitMQConfig rabbitMQConfig, string requestQueueName)
        {
            _factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                DispatchConsumersAsync = true
            };
            _requestQueueName = requestQueueName;
        }

        protected void WaitForResponse(AsyncEventHandler<BasicDeliverEventArgs> received)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (_channel = connection.CreateModel())
                {
                    _channel.QueueDeclare(_requestQueueName, true, false, false, null);
                    var consumer = new AsyncEventingBasicConsumer(_channel);
                    consumer.Received += received;
                    _channel.BasicConsume(_requestQueueName, true, consumer);

                    Console.WriteLine("Waiting for messages...");
                    while (true)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }

        protected abstract Task Received(object sender, BasicDeliverEventArgs e);

        protected void Publish(string exchangeName, string correlationId, string responseQueueName, byte[] item)
        {
            var responseProps = _channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;
            _channel.BasicPublish(exchangeName, responseQueueName, responseProps, item);
        }
    }
}
