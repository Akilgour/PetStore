using PetStore.OrderMonkey.Model;
using System.Collections.Generic;

namespace PetStore.OrderMonkey.Factorys
{
    public static class StockList
    {
        public static List<Stock> Create()
        {
            return new List<Stock>()
            {
                new Stock() { Id = 1, Name = "Alpacas food" },
                new Stock() { Id = 2, Name = "Camels food" },
                new Stock() { Id = 3, Name = "Cats food" },
                new Stock() { Id = 4, Name = "Cattle food" },
                new Stock() { Id = 5, Name = "Dogs food" },
                new Stock() { Id = 6, Name = "Donkeys food" },
                new Stock() { Id = 7, Name = "Ferrets food" },
                new Stock() { Id = 8, Name = "Goats food" },
                new Stock() { Id = 9, Name = "Hedgehogs food" },
                new Stock() { Id = 10, Name = "Horses food" },
                new Stock() { Id = 11, Name = "Llamas food" },
                new Stock() { Id = 12, Name = "Pigs food" },
                new Stock() { Id = 13, Name = "Rabbits food" },
                new Stock() { Id = 14, Name = "Foxes food" },
                new Stock() { Id = 15, Name = "Rats food" },
                new Stock() { Id = 16, Name = "Mice food" },
                new Stock() { Id = 17, Name = "Hamsters food" },
                new Stock() { Id = 18, Name = "Guinea pigs food" },
                new Stock() { Id = 19, Name = "Gerbils, food" },
                new Stock() { Id = 20 , Name = "Chinchillas food" },
                new Stock() { Id = 21 , Name = "Sheep food" },
                new Stock() { Id = 22 , Name = "Chicken food" },
                new Stock() { Id = 23 , Name = "Parrots food" },
                new Stock() { Id = 24 , Name = "Budgie food" },
                new Stock() { Id = 25 , Name = "Cockatiel. food" },
                new Stock() { Id = 26 , Name = "Turkeys food" },
                new Stock() { Id = 27 , Name = "Ducks food" },
                new Stock() { Id = 28 , Name = "Quails food" },
                new Stock() { Id = 29 , Name = "Finches food" },
                new Stock() { Id = 30 , Name = "Canaries food" },
                new Stock() { Id = 31 , Name = "Goldfish food" },
                new Stock() { Id = 32 , Name = "Koi food" },
                new Stock() { Id = 33 , Name = "Guppy food" },
                new Stock() { Id = 34 , Name = "Molly food" },
                new Stock() { Id = 35 , Name = "Bees food" },
                new Stock() { Id = 36 , Name = "Silk worm food" }
            };
        }
    }
}