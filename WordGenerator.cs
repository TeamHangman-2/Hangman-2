namespace Hangman
{
    using System;

    public static class WordGenerator
    {
        private static readonly string[] AllWords = 
                                                    {
                                                        "computer", "programmer", "software", "debugger", "compiler",
                                                        "developer", "algorithm", "array", "method", "variable"
                                                    };

        private static Random random = new Random();

        public static Word GetRandomWord()
        {
            var indexOfRndWord = random.Next(0, AllWords.Length);
            var randomWord = new Word(AllWords[indexOfRndWord]);

            return randomWord;
        }
    }
}
