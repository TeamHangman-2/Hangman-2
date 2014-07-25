namespace HangmanConsole
{
    using System;

    using Hangman.IO;

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


#warning: TODO: remove this main method when the HangmanLauncher class is added to git repository
        static void Main()
        {

        }

    }
}
