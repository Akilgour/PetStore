using System;

namespace PetStore.OrderItem.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PetStore.OrderItem.Server");
            try
            {
                var application = new Application();
                application.Run(args);
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
