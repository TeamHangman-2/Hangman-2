namespace HangmanTests
{
    using System;

    using Hangman;
    using NUnit.Framework;

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

            wordObject.UpdateWordOnScreen('u');

            string expectedMask = "____u___";

            Assert.AreEqual(expectedMask, wordObject.WordOnScreen);
        }

        [Test]
        public void CanProcessMultipleGuessedLetters()
        {
            string wordString = "banana";
            var wordObject = new Word(wordString);

            wordObject.UpdateWordOnScreen('a');

            string expectedMask = "_a_a_a";

            Assert.AreEqual(expectedMask, wordObject.WordOnScreen);
        }

        [Test]
        public void CanRevealLetters()
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

        [Test]
        [Test]
        public void CanGuessWholeWord()
        {
            string wordString = "something";
            var wordObject = new Word(wordString);

            Assert.True(wordObject.GuessWholeWord("something"));
        }

        [Test]
        public void CanIndicateThatWordIsGuessed()
        {
            string wordString = "yes";
            var wordObject = new Word(wordString);

            wordObject.UpdateWordOnScreen('y');
            wordObject.UpdateWordOnScreen('e');
            wordObject.UpdateWordOnScreen('s');

            Assert.True(wordObject.EntireWordIsRevealed);
        }

        [Test]
        public void CanShowHiddenLettersCount()
        {
            string wordString = "word";
            var wordObject = new Word(wordString);

            wordObject.UpdateWordOnScreen('w');
            wordObject.UpdateWordOnScreen('d');

            Assert.AreEqual(2, wordObject.NumberOfHiddenLetters);
        }

        [Test]
        public void CanCheckIfContainsLetter()
        {
            string wordString = "something";
            var wordObject = new Word(wordString);

            Assert.True(wordObject.Containsletter('s'));
            Assert.True(wordObject.Containsletter('o'));
            Assert.True(wordObject.Containsletter('m'));
            Assert.True(wordObject.Containsletter('e'));
            Assert.True(wordObject.Containsletter('t'));
            Assert.True(wordObject.Containsletter('h'));
            Assert.True(wordObject.Containsletter('i'));
            Assert.True(wordObject.Containsletter('n'));
            Assert.True(wordObject.Containsletter('g'));
        }

        [Test]
        public void CanGetLength()
        {
            string wordString = "something";
            string anotherWordString = "aWord";

            var wordObject = new Word(wordString);
            var justAnotherWord = new Word(anotherWordString);

            Assert.AreEqual(9, wordObject.Length);
            Assert.AreEqual(5, justAnotherWord.Length);
        }
    }
}
