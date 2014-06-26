using System;
using System.Linq;

namespace Hangman
{
    public class WordGuesser
    {
        private string word;

        public WordGuesser(string word)
        {
            this.Word = word;
        }
        public string Word
        {
            get { return this.word; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Word cannot be null!");
                }

                if (value == string.Empty)
                {
                    throw new ArgumentException("Word cannot be empty string!");
                }
            }
        }

        public static bool IsExited;
       //2 methods from WordInitializator must be moved here!
        public void GuessLetter()
        {
            Console.WriteLine("Enter your guess: ");
            string supposedCharOrCommand = Console.ReadLine();

            if (supposedCharOrCommand.Length == 1)
            {
                char supposedChar = supposedCharOrCommand[0];
                GameManager.ProccessGuess(Word, supposedChar);
            }
            else if (supposedCharOrCommand.Equals("help"))
            {
                CommandExecuter.RevealTheNextLetter(Word);
            }
            else if (supposedCharOrCommand.Equals("restart"))
            {
                CommandExecuter.Restart();
            }
            else if (supposedCharOrCommand.Equals("exit"))
            {
                CommandExecuter.Exit();
                return;
            }
            else if (supposedCharOrCommand.Equals("top"))
            {
                CommandExecuter.TopResults();
            }
        }             
    }
}
