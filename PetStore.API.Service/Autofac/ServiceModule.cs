using Autofac;
using PetStore.OrderItem.Client.Autoface;
using PetStore.StockDelivery.Client.Autofac;
using System.Linq;

namespace PetStore.API.Service.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new StockDeliveryClientModule());
            builder.RegisterModule(new OrderItemClientModule());

            //"ThisAssembly" means "any types in the same assembly as the module"
            builder.RegisterAssemblyTypes(ThisAssembly)
                 .Where(t => t.Name.EndsWith("Service"))
                 .AsImplementedInterfaces();
        }
    }
}