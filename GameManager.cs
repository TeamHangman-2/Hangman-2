using System;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class GameManager
    {
        
        //public string Word{get; set;}
        public static bool UsedHelp = false;
        public static int RevealedCount = 0;
        public static char[] GuessedLetters;//= new int[this.Word.Length];

        static int mistakesCount = 0;

        //public static void InitializeGame(string word)
        //{
        //    Console.WriteLine(IntroductingMessage);
           
        //    GuessedLetters = new char[word.Length];
        //    StringBuilder hiddenWord = new StringBuilder();

        //    for (int i = 0; i < word.Length; i++)
        //    {
        //        GuessedLetters[i] = '$';
        //        hiddenWord.Append("_ ");
        //    }

        //    Console.WriteLine("The secret word is: ");
        //    Console.WriteLine(hiddenWord+"\n");
        //}

        //public static void RevealGuessedLetters(Word word)
        //{
        //    StringBuilder revealedWord = new StringBuilder();

        //    for (int i = 0; i < word.Length; i++)
        //    {
        //        if (GuessedLetters[i].Equals('$'))
        //        {
        //            revealedWord.Append("_ ");
        //        }
        //        else
        //        {
        //            revealedWord.Append(GuessedLetters[i].ToString() + " ");
        //        }
        //    }

        //    Console.WriteLine(revealedWord);
        //}

       
        public static void FinalizeGame(Word word)
        {
            Console.WriteLine("You won with {0} mistakes.", mistakesCount);
            RevealGuessedLetters(word);
            int freeScoreboardPosition = 4;

            for (int i = 0; i < 4; i++)
            {
                if (CommandExecuter.ScoreBoard[i] == null)
                {
                    freeScoreboardPosition = i;
                    break;
                }
            }

            if ((CommandExecuter.ScoreBoard[freeScoreboardPosition] == null|| 
                 mistakesCount <= CommandExecuter.ScoreBoard[freeScoreboardPosition].NumberOfMistakes)
                  && UsedHelp == false)
            {
                Console.WriteLine("Please enter your name for the top scoreboard:");
                string playerName = Console.ReadLine();
                Player newResult = new Player(playerName, mistakesCount);
                CommandExecuter.ScoreBoard[freeScoreboardPosition] = newResult;

                for (int i = freeScoreboardPosition; i > 0; i--)
                {
                    if (CommandExecuter.ScoreBoard[i].CompareTo(CommandExecuter.ScoreBoard[i - 1]) < 0)
                    {
                        Player betterScore = CommandExecuter.ScoreBoard[i];
                        CommandExecuter.ScoreBoard[i] = CommandExecuter.ScoreBoard[i - 1];
                        CommandExecuter.ScoreBoard[i - 1] = betterScore;
                    }
                }
            }

            RevealedCount = 0;
            mistakesCount = 0;
            UsedHelp = false;
        }
    }
}
