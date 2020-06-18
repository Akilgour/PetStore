namespace PetStore.Shared.RabbitMQ.Factorys
{
    public static class RabbitMQConfigFactory
    {
        public static RabbitMQConfig Create() => new RabbitMQConfig("localhost", "guest", "guest");
    }
}
