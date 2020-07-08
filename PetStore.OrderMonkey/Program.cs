using Autofac;
using PetStore.OrderMonkey.Autofac;
using System;

namespace PetStore.OrderMonkey
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var message = "PetStore OrderMonkey";
            Console.Title = message;
            Console.WriteLine(message);

            try
            {
                var container = AutofacConfiguration.Configure();
                var task = container.Resolve<Application>().Run( );
                task.Wait();
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