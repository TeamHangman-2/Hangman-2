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
        public void GuessLetter(string guessedLetter)
        {         
            if (guessedLetter.Length == 1)
            {
                char supposedChar = guessedLetter[0];
                GameManager.ProccessGuess(Word, supposedChar);
            }
            else
            {
                //TODO:Make class for execute commands
                CommandExecuter(guessedLetter);
            }
        } 
        
        public void CommandExecuter(string command)
        {
            switch (command)
            {
                case "help": CommandExecuter.RevealTheNextLetter(Word); break;
                case "restart": CommandExecuter.Restart(); break;
                case "exit": CommandExecuter.Exit(); break;
                case "top": CommandExecuter.TopResults(); break;
                default:
                    throw new InvalidOperationException("Invalid command!");
            }
        }
    }
}
