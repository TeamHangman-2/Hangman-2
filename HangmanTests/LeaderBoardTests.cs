using Hangman;
using Hangman.IO;
using NSubstitute;
using NUnit.Framework;
using System;

namespace HangmanTests
{
    public class LeaderBoardTests
    {
        [Test]
        public void ShouldLazyLoadTopPlayers()
        {
            var fakeStorage = CreateFakeStorageProvider();
            var leaderBoard = new LeaderBoard(fakeStorage);

            var players = leaderBoard.TopPlayers;

            fakeStorage.Received().GetTop(Arg.Any<int>());
        }

#warning TODO: implement these tests:
        public void ShouldSaveDataAfterPlayerCommit()
        {
         
        }

        public void ShouldAddPlayerWithProperScore()
        {

        }

        /// <summary>
        /// Create a fake IStorageProvider suitable for leaderBoard testing
        /// </summary>
        private static IStorageProvider<string, string> CreateFakeStorageProvider()
        {
            var fakeStorage = Substitute.For<IStorageProvider<string, string>>();
            string[] fakeData = {"player1", "player2", "player3", "player4"};

            fakeStorage.GetTop(Arg.Any<int>()).Returns(fakeData);

            return fakeStorage;
        }
    }
}
