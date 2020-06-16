using Autofac;
using PetStore.StockDelivery.Client.Autofac;
using System.Reflection;

namespace PetStore.StockDelivery.Autofac
{
    public class AutofacConfiguration 
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

           

            builder.RegisterModule(new StockDeliveryClientModule( ));

            // Register the application class
            builder.RegisterType<Application>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            // Set the dependency resolver to be Autofac.
            return builder.Build();


        }
    }
}
