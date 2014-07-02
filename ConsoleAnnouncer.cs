using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    //"You have already revelaed the letter"
    //Console.WriteLine();
    public class ConsoleAnnouncer : IGameAnnouncer
    {
        private const string IntroductingMessage = "Welcome to “Hangman” game. Please try to guess my secret word.\nUse 'top' to view the top scoreboard, 'restart' to start a new game,'help' to cheat and 'exit' to quit the game.";
        public void OutputGameStartMessage()
        {
            Console.WriteLine(IntroductingMessage);
        }

        public void OutputGameWonMessage()
        {
            throw new NotImplementedException();
        }

        public void OutputGameLostMessage()
        {
            throw new NotImplementedException();
        }

        public void OutputAvailableCommands()
        {
            throw new NotImplementedException();
        }

        public void OutputGuessesMade(string guessesMade)
        {
            Console.WriteLine("You have already revelaed the letter {0}", guessesMade);
        }

        public void OutputWordVisualisation(char[] p)
        {
            throw new NotImplementedException();
        }

        public void OutputIntroMessage()
        {
            throw new NotImplementedException();
        }


        public void OutputRepeatingGuessMessage()
        {
            throw new NotImplementedException();
        }
    }
}
