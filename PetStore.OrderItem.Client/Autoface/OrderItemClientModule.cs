using Autofac;
using PetStore.Shared.RabbitMQ;
using System.Linq;

namespace PetStore.OrderItem.Client.Autoface
{
    public class OrderItemClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //"ThisAssembly" means "any types in the same assembly as the module"
            builder.RegisterAssemblyTypes(ThisAssembly)
                 .WithParameter("rabbitMQConfig", new RabbitMQConfig("localhost", "guest", "guest"))
                 .Where(t => t.Name.EndsWith("Client"))
                 .AsImplementedInterfaces();
        }
    }
}