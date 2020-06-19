using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Shared.RabbitMQ
{
    public abstract class BaseSendClient
    {
        protected ConnectionFactory factory;
        private readonly string _requestQueueName;

        public BaseSendClient(RabbitMQConfig rabbitMQConfig, string requestQueueName)
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

        protected void WaitForResponse(AsyncEventHandler<BasicDeliverEventArgs> received)
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
                    consumer.Received += received;
                    channel.BasicConsume(_requestQueueName, true, consumer);

                    Console.WriteLine("Waiting for messages...");
                    while (true)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }

        protected abstract Task Received(object sender, BasicDeliverEventArgs e);
    }
}
