using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var textMessage01 = new TextMessage { Id = 1, Text = "A1" };
        var textMessage02 = new TextMessage { Id = 2, Text = "B2" };

        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicPublish("", "hello", null, textMessage01.Serialize());
            channel.BasicPublish("", "hello", null, textMessage02.Serialize());
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

        Get();
    }

    public static void Get()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message1 = (TextMessage)body.DeSerialize(typeof(TextMessage));
                Console.WriteLine($" id {message1.Id}  Text {message1.Text}");
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}