using Autofac;
using PetStore.StockDelivery.Autofac;
using System;

namespace PetStore.StockDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

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
