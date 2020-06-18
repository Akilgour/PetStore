using Autofac;
using PetStore.Shared;
using PetStore.Shared.RabbitMQ;
using PetStore.StockDelivery.Manager.Autofac;

namespace PetStore.StockDelivery.Client.Autofac
{
    public class StockDeliveryClientModule : Module
    {
        public StockDeliveryClientModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            var rabbitMQConfig = new RabbitMQConfig("localhost", "guest", "guest");

            builder.RegisterModule(new StockDeliveryManagerModule());

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Client"))
                .WithParameter("rabbitMQConfig", rabbitMQConfig)
                .AsImplementedInterfaces();
        }

    }
}
