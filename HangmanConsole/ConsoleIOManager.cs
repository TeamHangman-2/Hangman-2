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

        public void Print(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
