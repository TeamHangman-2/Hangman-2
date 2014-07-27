namespace Hangman.WordGeneration
{
    using System;
    using System.Collections.Generic;

    public class RandomWordGenerator : IWordGenerator
    {
        private static Random random = new Random();
        private readonly IList<string> allWords;

        public RandomWordGenerator()
        {
            this.allWords = new string[]
            {
                "computer", "programmer", "software", "debugger", "compiler",
                "developer", "algorithm", "array", "method", "variable"
            };
        }

        public RandomWordGenerator(IList<string> availableWords)
        {
            if (availableWords == null)
            {
                throw new ArgumentNullException("The available words collection cannot be null");
            }

            if (availableWords.Count == 0)
            {
                throw new ArgumentException("The available words collection cannot be empty");
            }

            this.allWords = availableWords;
        }

        public Word GetWord()
        {
            var indexOfRndWord = random.Next(0, this.allWords.Count);
            var randomWord = new Word(this.allWords[indexOfRndWord]);

            return randomWord;
        }
    }
}
