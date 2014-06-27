namespace Hangman
{
    using System;

    public static class WordGenerator
    {
        private static readonly string[] allWords = {
                                                        "computer", "programmer", "software", "debugger", "compiler",
                                                        "developer", "algorithm", "array", "method", "variable"
                                                    };

        private static Random rnd = new Random();

        public Word GetRandomWord()
        {
            var indexOfRndWord = rnd.Next(0, allWords.Length);
            var randomWord = new Word();


            return randomWord;
        }
    }
}
