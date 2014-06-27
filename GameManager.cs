using System;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class GameManager
    {
        private const string IntroductingMessage = "Welcome to “Hangman” game. Please try to guess my secret word.\nUse 'top' to view the top scoreboard, 'restart' to start a new game,'help' to cheat and 'exit' to quit the game.";
        //public string Word{get; set;}
        public static bool UsedHelp = false;
        public static int RevealedCount = 0;
        public static char[] GuessedLetters;//= new int[this.Word.Length];

        static int mistakesCount = 0;

        public static void InitializeGame(string word)
        {
            Console.WriteLine(IntroductingMessage);
           
            GuessedLetters = new char[word.Length];
            StringBuilder hiddenWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                GuessedLetters[i] = '$';
                hiddenWord.Append("_ ");
            }

            Console.WriteLine("The secret word is: ");
            Console.WriteLine(hiddenWord+"\n");
        }

        public static void RevealGuessedLetters(string word)
        {
            StringBuilder revealedWord = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                if (GuessedLetters[i].Equals('$'))
                {
                    revealedWord.Append("_ ");
                }
                else
                {
                    revealedWord.Append(GuessedLetters[i].ToString() + " ");
                }
            }

            Console.WriteLine(revealedWord);
        }

        public static void ProccessGuess(string word, char charSupposed)
        {
            StringBuilder wordInitailized = new StringBuilder();
            int letterApperance = 0;

            if (GuessedLetters.Contains<char>(charSupposed))
            {
                Console.WriteLine("You have already revelaed the letter {0}", charSupposed);
            }
            else
            {
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
               
                if (RevealedCount == word.Length)
                {
                    FinalizeGame(word);
                    CommandExecuterOld.Restart();
                }

                Console.WriteLine("The secret word is:");
                RevealGuessedLetters(word);
            }
        }
        public static void FinalizeGame(string word)
        {
            Console.WriteLine("You won with {0} mistakes.", mistakesCount);
            RevealGuessedLetters(word);
            int freeScoreboardPosition = 4;

            for (int i = 0; i < 4; i++)
            {
                if (CommandExecuterOld.ScoreBoard[i] == null)
                {
                    freeScoreboardPosition = i;
                    break;
                }
            }

            if ((CommandExecuterOld.ScoreBoard[freeScoreboardPosition] == null|| 
                 mistakesCount <= CommandExecuterOld.ScoreBoard[freeScoreboardPosition].NumberOfMistakes)
                  && UsedHelp == false)
            {
                Console.WriteLine("Please enter your name for the top scoreboard:");
                string playerName = Console.ReadLine();
                Player newResult = new Player(playerName, mistakesCount);
                CommandExecuterOld.ScoreBoard[freeScoreboardPosition] = newResult;

                for (int i = freeScoreboardPosition; i > 0; i--)
                {
                    if (CommandExecuterOld.ScoreBoard[i].CompareTo(CommandExecuterOld.ScoreBoard[i - 1]) < 0)
                    {
                        Player betterScore = CommandExecuterOld.ScoreBoard[i];
                        CommandExecuterOld.ScoreBoard[i] = CommandExecuterOld.ScoreBoard[i - 1];
                        CommandExecuterOld.ScoreBoard[i - 1] = betterScore;
                    }
                }
            }

            RevealedCount = 0;
            mistakesCount = 0;
            UsedHelp = false;
        }
    }
}
