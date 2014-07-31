namespace HangmanConsole
{
    using System;

    using Hangman.IO;

    /// <summary>
    /// Class that manages printing and reading from console
    /// </summary>
    internal class ConsoleIOManager : IOManager
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

        public void ClearOutputWindow()
        {
            Console.Clear();
        }
    }
}
