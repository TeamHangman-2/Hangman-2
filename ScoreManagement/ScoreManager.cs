namespace Hangman.ScoreManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hangman.IO;

    /// <summary>
    /// Manage scores from player's and provide leader board
    /// </summary>
    public class ScoreManager : IScoreManager
    {
        public const int LeaderBoardLength = 5;
        public const string LeaderBoardKey = "_LeaderBoard";
        private const char SeparatorCharacter = ',';

        private IStorageProvider<string, string> storage;
        private SortedList<int, PlayerScore> leaderboard;

        public ScoreManager(IStorageProvider<string, string> storage)
        {
            this.storage = storage;
        }

        private SortedList<int, PlayerScore> LeaderBoard
        {
            get
            {
                if (this.leaderboard == null)
                {
                    this.leaderboard = new SortedList<int, PlayerScore>();
                    this.LoadLeaderBoard();
                }

                return this.leaderboard;
            }
        }
        
        public IEnumerable<PlayerScore> GetLeaderBoard()
        {
            // casting the collection to IEnumerable so that it cannot be changed
            return (IEnumerable<PlayerScore>)this.LeaderBoard.Values;
        }

        /// <summary>
        /// Mathod that saves player's score to storage by name as key and points
        /// and number of mistakes as value.
        /// If storage does not contain player's name, new entry is add, otherwise storage updates 
        /// player's score.
        /// </summary>
        /// <param name="score">New player's score</param>
        public void SavePlayerScore(PlayerScore score)
        {
            this.VerifyAndAddToBoard(score);

            var newPlayerData = string.Join(
                SeparatorCharacter.ToString(), 
                score.Points, 
                score.NumberOfMistakes);

            if (this.storage.ContainsKey(score.PlayerName))
            {
                var playerScore = this.LoadPlayerRecord(score.PlayerName);

                if (playerScore.Points < score.Points)
                {
                    this.storage.UpdateEntry(score.PlayerName, newPlayerData);
                }
            }
            else
            {
                this.storage.AddEntry(score.PlayerName, newPlayerData);
            }
        }

        /// <summary>
        /// Method that loads player score from storage by pllayer name.
        /// If storage does not contain player name, mathod return new player score with
        /// default points and number of mistakes.
        /// </summary>
        /// <param name="playerName">Name of player</param>
        /// <returns>New player's score</returns>
        public PlayerScore LoadPlayerRecord(string playerName)
        {
            if (this.storage.ContainsKey(playerName))
            {
                var rawPlayerRecord = this.storage.LoadEntry(playerName)
                                                  .Split(SeparatorCharacter);

                var playerPoints = int.Parse(rawPlayerRecord[0]);
                var playerMistakes = int.Parse(rawPlayerRecord[1]);

                var result = new PlayerScore(playerName, playerPoints, playerMistakes);
                return result;
            }
            else
            {
                int defaultRecordPoints = default(int);
                int defaultRecordMistakes = default(int);
                return new PlayerScore(playerName, defaultRecordPoints, defaultRecordMistakes);
            }
        }

        /// <summary>
        /// Mathod that saves the leader board in the storage.
        /// </summary>
        private void SaveLeaderBoard()
        {
            StringBuilder leaderBoardString = new StringBuilder();

            foreach (var score in this.LeaderBoard)
            {
                var value = score.Value;
                string scoreData = string.Join(
                    SeparatorCharacter.ToString(), 
                    value.PlayerName,
                    value.Points, 
                    value.NumberOfMistakes);

                leaderBoardString.AppendLine(scoreData);
            }

            if (this.storage.ContainsKey(LeaderBoardKey))
            {
                this.storage.UpdateEntry(LeaderBoardKey, leaderBoardString.ToString());
            }
            else
            {
                this.storage.AddEntry(LeaderBoardKey, leaderBoardString.ToString());
            }
        }

        /// <summary>
        /// Mathod that adds the score to the leaderboard if the score is appropriate
        /// </summary>
        private void VerifyAndAddToBoard(PlayerScore score)
        {
            if (score.PlayerName == LeaderBoardKey)
            {
                throw new ArgumentException(string.Format(
                    "The player name may not be equal to {0}," +
                    "because the leader board data is saved to that key", 
                    LeaderBoardKey));
            }

            if (this.LeaderBoard.Count < LeaderBoardLength)
            {
                this.LeaderBoard.Add(score.Points, score);
                Task.Run((Action)this.SaveLeaderBoard);
            }
            else
            {
                int lastInBoard = this.LeaderBoard.Keys.Min();
                if (lastInBoard <= score.Points)
                {
                    this.LeaderBoard.Add(score.Points, score);
                    this.LeaderBoard.Remove(lastInBoard);
                    Task.Run((Action)this.SaveLeaderBoard);
                }
            }
        }

        /// <summary>
        /// Mathod that loads the leader board from the storage
        /// </summary>
        private void LoadLeaderBoard()
        {
            if (this.storage.ContainsKey(LeaderBoardKey))
            {
                var separator = new string[] { Environment.NewLine };

                var rawData = this.storage.LoadEntry(LeaderBoardKey).Split(separator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var entry in rawData)
                {
                    var segments = entry.Split(SeparatorCharacter);
                    var playerName = segments[0];
                    var points = int.Parse(segments[1]);
                    var mistakesCount = int.Parse(segments[2]);

                    var scoreObject = new PlayerScore(playerName, points, mistakesCount);

                    this.leaderboard.Add(points, scoreObject);
                }
            }
        }
    }
}
