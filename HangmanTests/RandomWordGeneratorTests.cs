using Hangman;
using Hangman.WordGeneration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanTests
{
    public class RandomWordGeneratorTests
    {
        [Test]
        public void RejectsNullWordsCollection()
        {
            Assert.Throws<ArgumentNullException>(() => new RandomWordGenerator(null));
        }

        [Test]
        public void RejectsEmptyWordsCollection()
        {
            Assert.Throws<ArgumentException>(() => new RandomWordGenerator(new List<string>()));
        }

        [Test]
        public void ShouldReturnWord()
        {
            var generator = new RandomWordGenerator(new string[] { "word" });

            var word = generator.GetWord();

            Assert.NotNull(word);
            Assert.AreEqual(4, word.Length);
        }

        [Test]
        public void ShouldGenerateDifferentWords()
        {
            var generator = new RandomWordGenerator();

            var thousandWords = new List<Word>(1000);

            for (int i = 0; i < 1000; i++)
            {
                thousandWords.Add(generator.GetWord());
            }

            // assert that there are at least two different words
            // by checking that at least two have different lengths
            Assert.True(thousandWords.Any(x => x.Length != thousandWords.First().Length));
        }

    }
}
