namespace PetStore.Shared
{
    public class RabbitMQConfig
    {
        public RabbitMQConfig(string hostName, string userName, string password)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
        }

        public string HostName { get; }
        public string UserName { get; }
        public string Password { get; }
    }
}