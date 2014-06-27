using System;
using System.Linq;

namespace Hangman
{
    public class CommandExecuterOld
    {
        public static Player[] ScoreBoard = new Player[5];
        private const char UnrevealedLetter = '$';

        public static void RevealTheNextLetter(string word)
        {
            char firstUnrevealedLetter = UnrevealedLetter;

            for (int i = 0; i < word.Length; i++)
            {
                if (GameManager.GuessedLetters[i].Equals(UnrevealedLetter))
                {
                    firstUnrevealedLetter = word[i];
                    break;
                }
            }

            Console.WriteLine("OK, I reveal for you the next letter {0}.", firstUnrevealedLetter);
            GameManager.ProccessGuess(word, firstUnrevealedLetter);
            GameManager.UsedHelp = true;
        }
        public static void Restart()
        {
            string word = WordSelector.SelectRandomWord();
            GameManager.InitializeGame(word);
            WordGuesser wg = new WordGuesser(word);


            while (GameManager.RevealedCount < word.Length && WordGuesser.IsExited == false)
            {
                string newLetter = Console.ReadLine();
                wg.GuessLetter(newLetter);
            }
        }
        public static void TopResults()
        {
            for (int i = 0; i < 5; i++)
            {
                if (ScoreBoard[i] != null)
                {
                    Console.WriteLine("{0}. {1} ---> {2}", i + 1, ScoreBoard[i].PlayerName, ScoreBoard[i].NumberOfMistakes);
                }
            }
        }
        public static void Exit()
        {
            Console.WriteLine("Good bye!");
            WordGuesser.IsExited = true;
        }

    }
}
