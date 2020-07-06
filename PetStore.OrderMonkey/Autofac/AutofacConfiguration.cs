using Autofac;
using PetStore.OrderItem.Manager.Autofac;
using System.Reflection;

namespace PetStore.OrderMonkey.Autofac
{
    public class AutofacConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new OrderItemManagerModule());

            // Register the application class
            builder.RegisterType<Application>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            // Set the dependency resolver to be Autofac.
            return builder.Build();
        }
    }
}