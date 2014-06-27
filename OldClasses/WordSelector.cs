using System;
using System.Linq;

namespace Hangman
{
    class WordSelector
    {
        private static readonly string[] words =
            {"computer", "programmer", "software", "debugger", "compiler",
             "developer", "algorithm", "array", "method", "variable" };
            
        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string SelectRandomWord()
        {
            int randomPosition = RandomNumber(0, words.Length);
            string randomlySelectedWord = words.ElementAt(randomPosition);

            return randomlySelectedWord;
        
        }
        static void Main(string[] args)
        {
            CommandExecuterOld.Restart();
                     
        }
    }
}
