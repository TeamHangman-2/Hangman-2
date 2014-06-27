namespace Hangman
{
    using System;

    public static class WordGenerator
    {
        private static readonly string[] allWords = {
                                                        "computer", "programmer", "software", "debugger", "compiler",
                                                        "developer", "algorithm", "array", "method", "variable"
                                                    };

        private static Random random = new Random();

        public static Word GetRandomWord()
        {
            var indexOfRndWord = random.Next(0, allWords.Length);
            var randomWord = new Word(allWords[indexOfRndWord]);

            return randomWord;
        }
    }
}
