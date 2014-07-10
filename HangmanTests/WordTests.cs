using Hangman;
using System;
using NUnit.Framework;

namespace HangmanTests
{
    public class WordTests
    {
        [TestCase("1234")]
        [TestCase("hi there")]
        [TestCase("--__--")]
        [TestCase("wr0ng!")]
        public void RejectInvalidWords(string word)
        {
            Assert.Throws<ArgumentException>(() => new Word(word));
        }

        [TestCase("validWord")]
        [TestCase("computer")]
        [TestCase("test")]
        [TestCase("unit")]
        public void AcceptValidWords(string word)
        {
            var wordObject = new Word(word);
            Assert.NotNull(wordObject);
        }

        [Test]
        public void ShouldHideWord()
        {
            string wordString = "justAWord";
            var wordObject = new Word(wordString);

            string expectedMask = new string('_', wordString.Length);
            Assert.AreEqual(expectedMask, wordObject.WordOnScreen);
        }

        [Test]
        public void ProcessGuess()
        {
            string wordString = "computer";
            var wordObject = new Word(wordString);

            wordObject.GuessLetter('u');

            string expectedMask = "____u___";

            Assert.AreEqual(expectedMask, wordObject.WordOnScreen);
        }

        [Test]
        public void ProcessMultipleGuessedLetters()
        {
            string wordString = "banana";
            var wordObject = new Word(wordString);

            wordObject.GuessLetter('a');

            string expectedMask = "_a_a_a";

            Assert.AreEqual(expectedMask, wordObject.WordOnScreen);
        }

        [Test]
        public void RevealFiveLetters()
        {
            string wordString = "something";
            var wordObject = new Word(wordString);

            for (int i = 0; i < 5; i++)
            {
                wordObject.RevealOneLetter();
            }

            string expectedMask = "somet____";
            Assert.AreEqual(expectedMask, wordObject.WordOnScreen);
        }

    }
}
