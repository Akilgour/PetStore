using Autofac;
using PetStore.OrderItem.Manager.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Server.Autofac
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
