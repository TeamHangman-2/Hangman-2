using Hangman;
using Hangman.IO;
using NSubstitute;
using NUnit.Framework;
using System;


namespace HangmanTests
{
    public class PlayerScoreTests
    {
        [Test]
        public void ShouldUseStorageToLoadData()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore score = new PlayerScore("somePlayer", fakeStorage);
            score.LoadRecordData();

            // Assert that LoadEntry method was called
            fakeStorage.Received().LoadEntry(Arg.Any<string>());
        }

        [Test]
        public void ShouldLazyLoadRecordPoints()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore score = new PlayerScore("somePlayer", fakeStorage);
            int recordPoints = score.RecordPoints;

            // Assert that LoadEntry method was called
            fakeStorage.Received().LoadEntry(Arg.Any<string>());
        }

        [Test]
        public void ShouldLazyLoadRecordMistakes()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore score = new PlayerScore("somePlayer", fakeStorage);
            int recordMistakes = score.RecordMistakes;

            // Assert that LoadEntry method was called
            fakeStorage.Received().LoadEntry(Arg.Any<string>());
        }

        [Test]
        public void CanAddRecordFile()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore score = new PlayerScore("somePlayer", fakeStorage);
            score.SaveRecordData();

            // Assert that LoadEntry method was called
            fakeStorage.Received().AddEntry(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CanUpdateCurrentStats()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore score = new PlayerScore("somePlayer", fakeStorage);
            score.UpdateCurrentStats(8);

            // Assert that LoadEntry method was called
            Assert.AreEqual(8, score.NumberOfMistakes);
        }

        [Test]
        public void CanBeCompared()
        {
            var fakeStorage = CreateFakeStorageProvider();

            PlayerScore lesserScore = new PlayerScore("somePlayer", fakeStorage);
            PlayerScore largerScore = new PlayerScore("somePlayer", fakeStorage);
            lesserScore.UpdateCurrentStats(8);
            largerScore.UpdateCurrentStats(5);

            // Assert that LoadEntry method was called
            Assert.True(lesserScore.CompareTo(largerScore) > 0);
        }


        /// <summary>
        /// Create a substitute for IStorageProvider<string, string>
        /// </summary>
        private static IStorageProvider<string, string> CreateFakeStorageProvider()
        {
            var fakeStorage = Substitute.For<IStorageProvider<string, string>>();

            fakeStorage.LoadEntry(Arg.Any<string>()).
                ReturnsForAnyArgs(x => "10,20");

            return fakeStorage;
        }
    }
}