using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class ConsoleAnnouncer : IGameAnnouncer
    {
        private const string RepeatingGuessMessage = "You have already revelaed the letter";
        private const string WonMessage = "You won! Number of mistakes: ";
        private const string LooseMessage = "You loose!";
        private const string IntroductingMessage = "Welcome to “Hangman” game. Please try to guess my secret word.\n, , and '.";
        public void OutputGameStartMessage()
        {
            Console.WriteLine(IntroductingMessage);
        }

        public void OutputGameWonMessage(int numberOfMistakes)
        {
            Console.WriteLine(WonMessage+numberOfMistakes);
        }

        public void OutputGameLostMessage(int numberOfMistakes)
        {
            Console.WriteLine(LooseMessage);
        }

        public void OutputAvailableCommands()
        {
            StringBuilder allCommands = new StringBuilder();
            allCommands.Append(GameCommands.Exit + " -  quit the game");
            allCommands.Append(GameCommands.Help + " - reveal first hidden letter");
            allCommands.Append(GameCommands.Restart + " - start a new game");
            allCommands.Append(GameCommands.ShowResult + " - view the top scoreboard");

            Console.WriteLine(allCommands.ToString());
        }

        public void OutputGuessesMade(string guessesMade)
        {
            Console.WriteLine("You have already revelaed the letter {0}", guessesMade);
        }

        public void OutputWordVisualisation(char[] word)
        {
            Console.WriteLine(string.Join("",word));
        }
        public void OutputRepeatingGuessMessage()
        {
            Console.WriteLine(RepeatingGuessMessage);
        }
    }
}
