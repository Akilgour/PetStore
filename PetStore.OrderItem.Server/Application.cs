using PetStore.Domain.Models;
using PetStore.OrderItem.Manager.Manager.Interface;
using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Server
{
    public class Application
    {
        private static IModel channel;
        private static string RequestQueueName = "OrderItem_RequestQueue";
        private readonly IOrderItemManager _orderItemManager;

        public Application(IOrderItemManager orderItemManager)
        {
            _orderItemManager = orderItemManager;
        }

        public void Run(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };

            using (var connection = factory.CreateConnection())
            {
                using (channel = connection.CreateModel())
                {
                    channel.QueueDeclare(RequestQueueName, true, false, false, null);
                    var consumer = new AsyncEventingBasicConsumer(channel);
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

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var stockOrder = (StockOrder)e.Body.ToArray().DeSerialize(typeof(StockOrder));
            var correlationId = e.BasicProperties.CorrelationId;
            string responseQueueName = e.BasicProperties.ReplyTo;
            Console.WriteLine($"Received: {stockOrder.OrderNumber} with CorrelationId {correlationId}");
            var responseMessage = await _orderItemManager.Order(stockOrder);
            Publish(responseMessage, correlationId, responseQueueName, responseMessage.Serialize());
        }
        
        private void Publish(OrderResponse responseMessage, string correlationId, string responseQueueName, byte[] item)
        {
            const string exchangeName = ""; // default exchange
            var responseProps = channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;
            channel.BasicPublish(exchangeName, responseQueueName, responseProps, item);
        }
    }
}