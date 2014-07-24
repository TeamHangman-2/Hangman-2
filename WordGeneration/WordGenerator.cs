﻿namespace Hangman.WordGeneration
{
    using System;
    using System.Collections.Generic;

    public class RandomWordGenerator : IWordGenerator
    {
        private readonly IList<string> AllWords;

        private static Random random = new Random();

        public RandomWordGenerator()
        {
            this.AllWords = new string[]
            {
                "computer", "programmer", "software", "debugger", "compiler",
                "developer", "algorithm", "array", "method", "variable"
            };
        }


        public RandomWordGenerator(IList<string> availableWords)
        {
            this.AllWords = availableWords;
        }


        public Word GetWord()
        {
            var indexOfRndWord = random.Next(0, AllWords.Count);
            var randomWord = new Word(AllWords[indexOfRndWord]);

            return randomWord;
        }
    }
}
