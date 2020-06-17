using PetStore.Domain.Models;
using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Threading;

namespace PetStore.OrderItem.Server
{
    public class Application
    {
        private static IModel channel;
        private static string RequestQueueName = "OrderItem_RequestQueue";

        public void Run(string[] args)
        {
            Console.Title = "RabbitMQ RPC Server";

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (channel = connection.CreateModel())
                {

                    channel.QueueDeclare(RequestQueueName, true, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(RequestQueueName, true, consumer);

                    Console.WriteLine("Waiting for messages...");
                    while (true)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var orderItem = (StockItem)e.Body.ToArray().DeSerialize(typeof(StockItem));
            var correlationId = e.BasicProperties.CorrelationId;
            string responseQueueName = e.BasicProperties.ReplyTo;

            Console.WriteLine($"Received: {orderItem.Name} with CorrelationId {correlationId}");

            var responseMessage = Reverse(orderItem);
            Publish(responseMessage, correlationId, responseQueueName, responseMessage.Serialize());
        }

        public static OrderResponse Reverse(StockItem orderItem)
        {
            return new OrderResponse()
            {
                Success = true,
                Message = $"Name : {orderItem.Name} Quantity : {orderItem.Quantity}"
            };
        }

        private static void Publish(OrderResponse responseMessage, string correlationId, string responseQueueName, byte[] item)
        {
            const string exchangeName = ""; // default exchange
            var responseProps = channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;
            channel.BasicPublish(exchangeName, responseQueueName, responseProps, item);
        }
    }
}
