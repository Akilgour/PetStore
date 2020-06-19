using Autofac;
using PetStore.StockDelivery.Autofac;
using System;

namespace PetStore.StockDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "PetStore StockDelivery Server";
            Console.Title = message;
            Console.WriteLine(message);

            try
            {
                var container = AutofacConfiguration.Configure();
                container.Resolve<Application>().Run( );
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
