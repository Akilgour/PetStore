using PetStore.Shared.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RPCShared.Models;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace RpcClient
{
    public class RpcClient : IDisposable
    {
        private bool disposed = false;
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        private ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>> pendingMessages;

        private const string requestQueueName = "requestqueue";
        private const string responseQueueName = "responsequeue";
        private const string exchangeName = ""; // default exchange

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();

            this.channel.QueueDeclare(requestQueueName, true, false, false, null);
            this.channel.QueueDeclare(responseQueueName, true, false, false, null);

            this.consumer = new EventingBasicConsumer(this.channel);
            this.consumer.Received += Consumer_Received;
            this.channel.BasicConsume(responseQueueName, true, consumer);

            this.pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>>();
        }

        public Task<OrderResponse> SendAsync(string message)
        {
            var tcs = new TaskCompletionSource<OrderResponse>();
            var correlationId = Guid.NewGuid().ToString();

            this.pendingMessages[correlationId] = tcs;

            this.Publish(message, correlationId);

            return tcs.Task;
        }

        private void Publish(string message, string correlationId)
        {
            var props = this.channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = responseQueueName;

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            this.channel.BasicPublish(exchangeName, requestQueueName, props, messageBytes);

            using (var colour = new ScopedConsoleColour(ConsoleColor.Yellow))
            {
                Console.WriteLine($"Sent: {message} with CorrelationId {correlationId}");
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var correlationId = e.BasicProperties.CorrelationId;
            var orderResponse = (OrderResponse)e.Body.ToArray().DeSerialize(typeof(OrderResponse));

            using (var colour = new ScopedConsoleColour(ConsoleColor.Yellow))
            {
                Console.WriteLine($"Received: {orderResponse.Message} with CorrelationId {correlationId}");
            }

            this.pendingMessages.TryRemove(correlationId, out var tcs);
            if (tcs != null)
            {
                tcs.SetResult(orderResponse);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                this.channel?.Dispose();
                this.connection?.Dispose();
            }

            this.disposed = true;
        }
    }
}