using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Shared.RabbitMQ
{
    public abstract class BaseReceiveReplyServer
    {
        private readonly string _requestQueueName;
        private readonly ConnectionFactory _factory;
        private IModel _channel;
        private readonly string _exchangeName;


        /// <summary>Initializes a new instance of the <see cref="BaseReceivedSendServer" /> class.</summary>
        /// <param name="rabbitMQConfig">The rabbit mq configuration.</param>
        /// <param name="requestQueueName">Name of the request queue.</param>
        /// <param name="exchangeName">Name of the exchange.</param>
        /// <exception cref="System.ArgumentNullException">rabbitMQConfig</exception>
        /// <exception cref="System.ArgumentException">message - requestQueueName</exception>
        public BaseReceiveReplyServer(RabbitMQConfig rabbitMQConfig, string requestQueueName, string exchangeName)
        {
            if (rabbitMQConfig is null)
            {
                throw new ArgumentNullException(nameof(rabbitMQConfig));
            }

            if (string.IsNullOrEmpty(requestQueueName))
            {
                throw new ArgumentException("message", nameof(requestQueueName));
            }

            _factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                DispatchConsumersAsync = true
            };

            _requestQueueName = requestQueueName;
            _exchangeName = exchangeName;
        }

        /// <summary>Waits for response.</summary>
        protected void WaitForResponse()
        {
            using (var connection = _factory.CreateConnection())
            {
                using (_channel = connection.CreateModel())
                {
                    _channel.QueueDeclare(_requestQueueName, true, false, false, null);
                    var consumer = new AsyncEventingBasicConsumer(_channel);
                    consumer.Received += Receive;
                    _channel.BasicConsume(_requestQueueName, true, consumer);

                    Console.WriteLine("Waiting for messages...");
                    while (true)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }

        /// <summary>Receive the specified sender. that has been sent through on the que sent in constructor param requestQueueName</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BasicDeliverEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        protected abstract Task Receive(object sender, BasicDeliverEventArgs e);

        /// <summary>Replys on the reply que in the publish header</summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="responseQueueName">Name of the response queue.</param>
        /// <param name="item">The item.</param>
        protected void Reply(string correlationId, string responseQueueName, byte[] item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (string.IsNullOrEmpty(correlationId))
            {
                throw new ArgumentException("message", nameof(correlationId));
            }

            if (string.IsNullOrEmpty(responseQueueName))
            {
                throw new ArgumentException("message", nameof(responseQueueName));
            }

            var responseProps = _channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;
            _channel.BasicPublish(_exchangeName, responseQueueName, responseProps, item);
        }
    }
}