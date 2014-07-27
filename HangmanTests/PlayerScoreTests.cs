namespace HangmanTests
{
    using System;

    using Hangman;
    using Hangman.IO;
    using Hangman.ScoreManagement;
    using NSubstitute;
    using NUnit.Framework;

    public class PlayerScoreTests
    {
        [Test]
        public void RejectsInvalidPoints()
        {
            var fakeStorage = CreateFakeStorageProvider();

            Assert.Throws<ArgumentException>(() => new PlayerScore("somePlayer", -5, 5));
        }

        [Test]
        public void RejectsInvalidMistakesCount()
        {
            var fakeStorage = CreateFakeStorageProvider();

            Assert.Throws<ArgumentException>(() => new PlayerScore("somePlayer", 50, -2));
        }

        [Test]
        public void RejectsInvalidName()
        {
            var fakeStorage = CreateFakeStorageProvider();

            Assert.Throws<ArgumentNullException>(() => new PlayerScore(null, 50, 10));
        }

        [Test]
        public void CanBeCompared()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore lesserScore = new PlayerScore("somePlayer", 50, 5);
            PlayerScore largerScore = new PlayerScore("somePlayer", 100, 2);

            Assert.Greater(largerScore, lesserScore);
        }

        /// <summary>
        /// Create a substitute for IStorageProvider<string, string>
        /// </summary>
        private static IStorageProvider<string, string> CreateFakeStorageProvider()
        {
            var fakeStorage = Substitute.For<IStorageProvider<string, string>>();

            fakeStorage.ContainsKey(Arg.Any<string>()).Returns(true);

            fakeStorage.LoadEntry(Arg.Any<string>())
                .ReturnsForAnyArgs(x => "10,20");

            return fakeStorage;
        }
    }
}