using System;

namespace PetStore.Shared.Helpers
{
    public class ScopedConsoleColourHelper : IDisposable
    {
        private ConsoleColor _oldColour;

        public ScopedConsoleColourHelper(ConsoleColor newColour)
        {
            SetColour(newColour);
        }

        private void SetColour(ConsoleColor newColour)
        {
            _oldColour = Console.ForegroundColor;
            Console.ForegroundColor = newColour;
        }

        public ScopedConsoleColourHelper()
        {
            var rand = new Random();
            int randomNumber = rand.Next(1, 6);

            switch (randomNumber)
            {
                case 1:
                    SetColour(ConsoleColor.Blue);
                    break;

                case 2:
                    SetColour(ConsoleColor.Green);
                    break;

                case 3:
                    SetColour(ConsoleColor.Cyan);
                    break;

                case 4:
                    SetColour(ConsoleColor.Red);
                    break;

                case 5:
                    SetColour(ConsoleColor.Magenta);
                    break;

                default:
                    SetColour(ConsoleColor.White);
                    break;
            }
        }

        public void Dispose()
        {
            Console.ForegroundColor = this._oldColour;
        }
    }
}