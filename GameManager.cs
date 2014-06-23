using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{

    // kojto mu se padne tozi kod da go opravya, moze da mi prati pozdravi na bate_goshko86@abv.bg. hahahaha@!@@!

    public class GameManager
    {
        //public string Word{get; set;}
        public static bool UsedHelp = false;
        public static int RevealedCount = 0;
        public static char[] GuessedLetters;//= new int[this.Word.Length];

        static int mistakesCount = 0;

        public static void InitializeGame(string word)
        {
            Console.WriteLine("Welcome to “Hangman” game. Please try to guess my secret word.");
            Console.WriteLine("Use 'top' to view the top scoreboard, 'restart' to start a new game,'help' to cheat and 'exit' to quit the game.");
            GuessedLetters = new char[word.Length];

            StringBuilder hiddenWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                GuessedLetters[i] = '$';
                hiddenWord.Append("_ ");
            }
            Console.WriteLine();
            Console.WriteLine("The secret word is: ");
            Console.WriteLine(hiddenWord);


            Console.WriteLine();
        }

        public static void RevealGuessedLetters(string word) // helper of the next function
        {
            StringBuilder partlyHiddenWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                if (GuessedLetters[i].Equals('$'))
                    partlyHiddenWord.Append("_ ");
                else
                    partlyHiddenWord.Append(GuessedLetters[i].ToString() + " ");
            }
            Console.WriteLine(partlyHiddenWord);
        }

        public static void ProccessGuess(string word, char charSupposed)
        {
            StringBuilder wordInitailized = new StringBuilder();
            int letterApperance = 0;

            if (GuessedLetters.Contains<char>(charSupposed))
            {
                Console.WriteLine("You have already revelaed the letter {0}", charSupposed);
                return;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i].Equals(charSupposed))
                {
                    GuessedLetters[i] = word[i];
                    letterApperance++;
                }
            }

            if (letterApperance == 0)
            {
                Console.WriteLine("Sorry! There are no unrevealed letters {0}", charSupposed);
                mistakesCount++;
            }
            else
            {
                Console.WriteLine("Good job! You revealed {0} letters.", letterApperance);
                RevealedCount += letterApperance;
            }
            Console.WriteLine();
            if (RevealedCount == word.Length) //check if the word is guessed
            {
                FinalizeGame(word);
                CommandExecuter.Restart();
            }
            Console.WriteLine("The secret word is:");
            RevealGuessedLetters(word);
        }
        //clear()
        public static void FinalizeGame(string word)
        {
            Console.WriteLine("You won with {0} mistakes.", mistakesCount);
            RevealGuessedLetters(word);
            Console.WriteLine();
            int freeScoreboardPosition = 4;
            for (int i = 0; i < 4; i++)
            {
                if (CommandExecuter.ScoreBoard[i] == null)
                {
                    freeScoreboardPosition = i;
                    break;
                }
            }

            if ((CommandExecuter.ScoreBoard[freeScoreboardPosition] == null //for free position
                  || mistakesCount <= CommandExecuter.ScoreBoard[freeScoreboardPosition].NumberOfMistakes)//when the 4th pos is not free)
                  && UsedHelp == false)
            {
                Console.WriteLine("Please enter your name for the top scoreboard:");
                string playerName = Console.ReadLine();
                CommandExecuter.PlayerMistakes newResult = new CommandExecuter.PlayerMistakes(playerName, mistakesCount);
                CommandExecuter.ScoreBoard[freeScoreboardPosition] = newResult;

                // Re-arrange the scoreboard:
                for (int i = freeScoreboardPosition; i > 0; i--)
                {
                    if (CommandExecuter.ScoreBoard[i].Compare(CommandExecuter.ScoreBoard[i - 1]) < 0)
                    {
                        //swap
                        CommandExecuter.PlayerMistakes betterScore = CommandExecuter.ScoreBoard[i];
                        CommandExecuter.ScoreBoard[i] = CommandExecuter.ScoreBoard[i - 1];
                        CommandExecuter.ScoreBoard[i - 1] = betterScore;
                    }
                }
            }
            RevealedCount = 0;
            mistakesCount = 0;
            UsedHelp = false;
        }
        //scoreboard a[5] s 5 rezultata
    }
}
