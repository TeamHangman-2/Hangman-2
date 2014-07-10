using Hangman.IO;
using System;

namespace HangmanConsole
{
    class ConsoleIOManager : IOManager
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
