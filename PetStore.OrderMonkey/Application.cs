using PetStore.OrderItem.Manager.Manager.Interface;
using PetStore.Shared.Helpers;
using System;

namespace PetStore.OrderMonkey
{
    public class Application
    {

        private readonly IOrderItemManager _orderItemManager;


        public Application(IOrderItemManager orderItemManager)

        {
            _orderItemManager = orderItemManager;
        }

        public void Run()
        {

            using (var colour = new ScopedConsoleColourHelper(ConsoleColor.Yellow))
            {
                Console.WriteLine("Everybody’s Got Something to Hide Except Me and My Monkey");
            }
        }
    }
}
