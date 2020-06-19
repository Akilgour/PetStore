using System;

namespace PetStore.Shared.Helpers
{
    public class ScopedConsoleColourHelper : IDisposable
    {
        private ConsoleColor _oldColour;

        public ScopedConsoleColourHelper(ConsoleColor newColour)
        {
            _oldColour = Console.ForegroundColor;
            Console.ForegroundColor = newColour;
        }

        public void Dispose()
        {
            Console.ForegroundColor = this._oldColour;
        }
    }
}