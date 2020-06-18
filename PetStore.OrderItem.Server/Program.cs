using Autofac;
using PetStore.OrderItem.Server.Autofac;
using System;

namespace PetStore.OrderItem.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "PetStore OrderItem Server";
            Console.Title = message;
            Console.WriteLine(message);

            try
            {
                var container = AutofacConfiguration.Configure();
                container.Resolve<Application>().Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex);
            }
            Console.WriteLine("End");
        }
    }
}
