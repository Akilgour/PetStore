using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using RabbitMQ.Client;
using System;

internal class Program
{
    private static ConnectionFactory _factory;
    private static IConnection _connection;
    private static IModel _model;
    private const string QueueName = "StandardQueue_ExampleQueue";

    private static void Main(string[] args)
    {
        var textMessage01 = new TextMessage { Id = 1, Text = "A1" };
        var textMessage02 = new TextMessage { Id = 2, Text = "B2" };
        CreateQueue();
        SendMessage(textMessage01);
        SendMessage(textMessage02);
    }

    private static void CreateQueue()
    {
        _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
        _connection = _factory.CreateConnection();
        _model = _connection.CreateModel();
        _model.QueueDeclare(QueueName, true, false, false, null);
    }

    private static void SendMessage(TextMessage message)
    {
        _model.BasicPublish("", QueueName, null, message.Serialize());
        Console.WriteLine("   Message Sent  ");
    }
}