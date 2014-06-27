﻿using System;
using System.Linq;

namespace Hangman
{
    public class CommandExecuter
    {
        //public static Player[] ScoreBoard = new Player[5];
        //private const char UnrevealedLetter = '$';
        private Word Word { get; set; }

        public CommandExecuter(Word newWord)
        {
            this.Word = newWord;
        }

        public int RevealLetterAt()
        {
            int result = -1;

            for (int i = 0; i < Word.WordOnScreen.Length; i++)
            {
                if (Word.WordOnScreen[i]=='_')
                {
                    result= i;
                    break;
                }
            }

            return result;
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
