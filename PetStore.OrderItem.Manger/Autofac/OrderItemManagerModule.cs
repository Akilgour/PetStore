using Autofac;
using PetStore.Data.Autofac;
using System.Linq;

namespace PetStore.OrderItem.Manger.Autofac
{
    public class OrderItemManagerModule : Module
    {
        public OrderItemManagerModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new PetStoreDataModule());

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Manager"))
                .AsImplementedInterfaces();
        }
    }
}
