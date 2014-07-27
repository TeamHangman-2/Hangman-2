namespace HangmanTests
{
    using System;
    using System.Linq;

    using Hangman.IO;
    using Hangman.ScoreManagement;
    using NSubstitute;
    using NUnit.Framework;

    public class ScoreManagerTests
    {
        [Test]
        public void ShouldCheckIfBoardExistsOnGet()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var scoreManager = new ScoreManager(fakeStorage);

            //// act
            scoreManager.GetLeaderBoard();

            //// assert
            fakeStorage.Received().ContainsKey(Arg.Any<string>());
        }

        [Test]
        public void ShouldLazyLoadLeaderBoard()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var scoreManager = new ScoreManager(fakeStorage);

            //// act
            scoreManager.GetLeaderBoard();

            //// assert
            fakeStorage.Received().LoadEntry(Arg.Any<string>());
        }

        [Test]
        public void CanSavePlayerScoreIfNoLeaderBoard()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var fakeScore = new PlayerScore("somePlayer", 5, 10);
            var scoreManager = new ScoreManager(fakeStorage);

            //// The fake storage contains no leaderBoard value stored
            fakeStorage.ContainsKey(ScoreManager.LeaderBoardKey).Returns(false);

            //// act
            scoreManager.SavePlayerScore(fakeScore);
            var leaderBoard = scoreManager.GetLeaderBoard();

            //// assert
            Assert.IsNotEmpty(leaderBoard);
            Assert.Contains(fakeScore, leaderBoard.ToList());
        }

        [Test]
        public void CanSavePlayerScoreOnFullLeaderBoard()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var fakeScore = new PlayerScore("somePlayer", 50, 2);
            var scoreManager = new ScoreManager(fakeStorage);

            //// act
            scoreManager.SavePlayerScore(fakeScore);
            var leaderBoard = scoreManager.GetLeaderBoard();

            //// assert
            Assert.IsNotEmpty(leaderBoard);
            Assert.Contains(fakeScore, leaderBoard.ToList());
        }

        [Test]
        public void CannotSavePlayerWithLowScore()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var fakeScore = new PlayerScore("somePlayer", 5, 20);
            var scoreManager = new ScoreManager(fakeStorage);

            //// act
            scoreManager.SavePlayerScore(fakeScore);
            var leaderBoard = scoreManager.GetLeaderBoard();

            //// assert
            Assert.IsNotEmpty(leaderBoard);
            Assert.False(leaderBoard.Contains(fakeScore));
        }

        [Test]
        public void CannotSavePlayerWithLeaderBoardKey()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var badPlayerName = new PlayerScore(ScoreManager.LeaderBoardKey, 5, 20);
            var scoreManager = new ScoreManager(fakeStorage);

            //// act & assert
            Assert.Throws<ArgumentException>(() => scoreManager.SavePlayerScore(badPlayerName));
        }

        [Test]
        public void ShouldSaveExistingLeaderBoardAfterUpdate()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var fakeScore = new PlayerScore("somePlayer", 50, 2);
            var scoreManager = new ScoreManager(fakeStorage);

            //// assume that a leaderboard exists
            fakeStorage.ContainsKey(ScoreManager.LeaderBoardKey).Returns(true);

            //// act
            scoreManager.SavePlayerScore(fakeScore);

            //// assert that leader board was updated
            fakeStorage.Received().UpdateEntry(ScoreManager.LeaderBoardKey, Arg.Any<string>());
        }

        [Test]
        public void ShouldCreateNewLeaderBoardIfNeededAfterUpdate()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var fakeScore = new PlayerScore("somePlayer", 50, 2);
            var scoreManager = new ScoreManager(fakeStorage);

            //// assume that no leader board is stored till now
            fakeStorage.ContainsKey(ScoreManager.LeaderBoardKey).Returns(false);

            //// act
            scoreManager.SavePlayerScore(fakeScore);

            //// assert that a leader board was created
            fakeStorage.Received().AddEntry(ScoreManager.LeaderBoardKey, Arg.Any<string>());
        }

        [Test]
        public void CanLoadPlayerRecord()
        {
            //// arrange
            var fakeStorage = CreateFakeStorageProvider();
            var scoreManager = new ScoreManager(fakeStorage);

            string somePlayerName = "playerName";

            //// act
            var record = scoreManager.LoadPlayerRecord(somePlayerName);

            //// assert that the score manager used the storage to load the data
            fakeStorage.Received().LoadEntry(somePlayerName);
            Assert.NotNull(record);
        }

        /// <summary>
        /// Create a substitute for IStorageProvider<string, string>
        /// </summary>
        /// <returns></returns>
        private static IStorageProvider<string, string> CreateFakeStorageProvider()
        {
            var fakeStorage = Substitute.For<IStorageProvider<string, string>>();

            fakeStorage.ContainsKey(Arg.Any<string>()).Returns(true);

            fakeStorage.LoadEntry(Arg.Any<string>())
                .ReturnsForAnyArgs(x => "10,5");

            fakeStorage.LoadEntry(ScoreManager.LeaderBoardKey).Returns(
@"player1,10,5
player2,20,5
player3,15,5
player4,12,5
player5,8,5");

            return fakeStorage;
        }
    }
}
