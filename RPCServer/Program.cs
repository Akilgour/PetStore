using PetStore.Shared.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RPCShared.Models;
using System;
using System.Text;

namespace RPCServer
{
    internal class Program
    {
        private static IModel channel;

        private static void Main(string[] args)
        {
            Console.Title = "RabbitMQ RPC Server";

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (channel = connection.CreateModel())
                {
                    const string requestQueueName = "requestqueue";
                    channel.QueueDeclare(requestQueueName, true, false, false, null);

                    // consumer

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(requestQueueName, true, consumer);

                    Console.WriteLine("Waiting for messages...");
                    Console.WriteLine("Press ENTER to exit.");
                    Console.WriteLine();
                    Console.ReadLine();
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var orderItem = (OrderItem)e.Body.ToArray().DeSerialize(typeof(OrderItem));
            var correlationId = e.BasicProperties.CorrelationId;
            string responseQueueName = e.BasicProperties.ReplyTo;

            Console.WriteLine($"Received: {orderItem.Name} with CorrelationId {correlationId}");

            var responseMessage = Reverse(orderItem);
            Publish(responseMessage, correlationId, responseQueueName, responseMessage.Serialize());
        }

        public static OrderResponse Reverse(OrderItem orderItem) // ref: https://stackoverflow.com/a/228060/983064
        {
            char[] charArray = orderItem.Name.ToCharArray();
            Array.Reverse(charArray);
            var msg =  new string(charArray);
            var orderResponse = new OrderResponse()
            {
                Success = true,
                Message = msg
            };
            return orderResponse;
        }

        private static void Publish(OrderResponse responseMessage, string correlationId, string responseQueueName, byte[] item)
        {
            const string exchangeName = ""; // default exchange
            var responseProps = channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;

            channel.BasicPublish(exchangeName, responseQueueName, responseProps, item);

            Console.WriteLine($"Sent: {responseMessage} with CorrelationId {correlationId}");
            Console.WriteLine();
        }
    }
}