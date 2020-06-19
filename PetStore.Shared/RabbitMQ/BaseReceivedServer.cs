using PetStore.Shared.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Shared.RabbitMQ
{
    /// <summary></summary>
    public abstract class BaseReceivedServer
    {
        protected ConnectionFactory factory;
        private readonly string _requestQueueName;

        /// <summary>Initializes a new instance of the <see cref="BaseReceivedServer" /> class.</summary>
        /// <param name="rabbitMQConfig">The rabbit mq configuration.</param>
        /// <param name="requestQueueName">Name of the request queue.</param>
        public BaseReceivedServer(RabbitMQConfig rabbitMQConfig, string requestQueueName)
        {
            factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                DispatchConsumersAsync = true
            };
            _requestQueueName = requestQueueName;
        }

        /// <summary>Waits for response, that has been sent through on the que sent in constructor param requestQueueName</summary>
        protected void WaitForResponse( )
        {
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _requestQueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    //   https://gigi.nullneuron.net/gigilabs/asynchronous-rabbitmq-consumers-in-net/
                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.Received += Received;
                    channel.BasicConsume(_requestQueueName, true, consumer);

                    using (var colour = new ScopedConsoleColourHelper(ConsoleColor.Green))
                    {
                        Console.WriteLine("Waiting for messages...");
                    }
                    while (true)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }

        /// <summary>The method that is called when something apear on the que</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BasicDeliverEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        protected abstract Task Received(object sender, BasicDeliverEventArgs e);
    }
}
