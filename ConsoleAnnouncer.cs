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
        private const string IntroductingMessage = "Welcome to “Hangman” game. Please try to guess my secret word.\nUse 'top' to view the top scoreboard, 'restart' to start a new game,'help' to cheat and 'exit' to quit the game.";
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
            allCommands.Append(GameCommands.Exit);
            allCommands.Append(GameCommands.Help);
            allCommands.Append(GameCommands.Restart);
            allCommands.Append(GameCommands.ShowResult);

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
