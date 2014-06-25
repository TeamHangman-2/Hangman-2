using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class CommandExecuter
    {
        public static Player [] ScoreBoard  = new Player[5];

        public static void RevealTheNextLetter(string word)
        {
            char firstUnrevealedLetter='$';
            
            for (int i = 0; i < word.Length; i++)
                if (GameManager.GuessedLetters[i] .Equals('$'))
                {
                    firstUnrevealedLetter = word[i];
                    break;
                }
            Console.WriteLine("OK, I reveal for you the next letter {0}.", firstUnrevealedLetter );
            GameManager.ProccessGuess (word, firstUnrevealedLetter);
            // Mark that the player used the 'help' command
            GameManager.UsedHelp = true; 
        }
        public static void Restart()
        {
            Console.WriteLine();
            string word = WordSelector.SelectRandomWord();
            //Console.WriteLine(word);
            GameManager.InitializeGame(word);
            WordGuesser wg = new WordGuesser() { Word = word };
            while (GameManager.RevealedCount < word.Length && WordGuesser.IsExited == false)
                wg.GuessLetter();


        }
        public static void TopResults()
        {
            Console.WriteLine();
            for (int i = 0; i < 5; i++)


                if(ScoreBoard[i] != null)
                    Console.WriteLine("{0}. {1} ---> {2}", i+1, ScoreBoard[i].PlayerName, ScoreBoard[i].NumberOfMistakes);
            Console.WriteLine();
        }
        public static void Exit()
        {
            Console.WriteLine("Good bye!");
            WordGuesser.IsExited = true;
            
            return;
        }

    }
}
