using PetStore.Domain.Models;
using PetStore.OrderItem.Manager.Manager.Interface;
using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using PetStore.Shared.RabbitMQ;
using PetStore.Shared.RabbitMQ.Factorys;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Server
{
    public class Application // : BaseReceiveReplyServer
    {
        private const string _requestQueueName = "OrderItem_RequestQueue";
        private readonly IOrderItemManager _orderItemManager;
        private const string _exchangeName = "";
        private static IModel channel;
        private   ConnectionFactory _factory;
        public Application(IOrderItemManager orderItemManager)
           //: base(RabbitMQConfigFactory.Create(), _requestQueueName, _exchangeName)
        {
          
            _orderItemManager = orderItemManager;
        }

        public void Run()
        {
            var rabbitMQConfig = RabbitMQConfigFactory.Create();
            _factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.HostName,
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                DispatchConsumersAsync = true
            };

            using (var connection = _factory.CreateConnection())
            {
                using (channel = connection.CreateModel())
                {
                    const string requestQueueName = _requestQueueName;
                    channel.QueueDeclare(requestQueueName,  true, false, false, null);
 

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.Received += Receive;
                    channel.BasicConsume(requestQueueName, true, consumer);

                    Console.WriteLine("Waiting for messages...");
                    Console.WriteLine("Press ENTER to exit.");
                    Console.WriteLine();
                    Console.ReadLine();
                }
            }
        }

        protected async Task Receive(object sender, BasicDeliverEventArgs e)
        {
            var stockOrder = (StockOrder)e.Body.ToArray().DeSerialize(typeof(StockOrder));
            var correlationId = e.BasicProperties.CorrelationId;
            string responseQueueName = e.BasicProperties.ReplyTo;

            Console.WriteLine($"Received: {stockOrder.OrderNumber} with CorrelationId {correlationId}");
            // var responseMessage = foo(stockOrder);
         
                 var responseMessage = await _orderItemManager.Order(stockOrder);
            Publish( correlationId, responseQueueName, responseMessage.Serialize());
        }

        

        private static void Publish(  string correlationId, string responseQueueName, byte[] item)
        {
            const string exchangeName = ""; // default exchange
            var responseProps = channel.CreateBasicProperties();
            responseProps.CorrelationId = correlationId;

            channel.BasicPublish(exchangeName, responseQueueName, responseProps, item);

            
        }
    }
}