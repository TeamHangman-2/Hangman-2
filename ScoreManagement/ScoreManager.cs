using Hangman.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.ScoreManagement
{
    public class ScoreManager : IScoreManager
    {
        private const char SeparatorCharacter = ',';

        public const int LeaderBoardLength = 5;
        public const string LeaderBoardKey = "_LeaderBoard";


        private IStorageProvider<string, string> storage;
        private SortedList<int, PlayerScore> leaderboard;

        private SortedList<int, PlayerScore> LeaderBoard
        {
            get
            {
                if (leaderboard == null)
                {
                    this.leaderboard = new SortedList<int, PlayerScore>();
                    LoadLeaderBoard();
                }
                return leaderboard;
            }
        }


        public ScoreManager(IStorageProvider<string, string> storage)
        {
            this.storage = storage;
        }

        public IEnumerable<PlayerScore> GetLeaderBoard()
        {
            // casting the collection to IEnumerable so that it cannot be changed
            return (IEnumerable<PlayerScore>)this.LeaderBoard.Values;
        }

        public void SavePlayerScore(PlayerScore score)
        {
            // Check if the player should be added to leader board
            VerifyAndAddToBoard(score);

            var newPlayerData = string.Join(SeparatorCharacter.ToString(),
                 score.Points, score.NumberOfMistakes);

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
        /// Saves the leader board in the storage
        /// </summary>
        private void SaveLeaderBoard()
        {
            StringBuilder leaderBoardString = new StringBuilder();

            foreach (var score in this.LeaderBoard)
            {
                var value = score.Value;
                string scoreData = string.Join(SeparatorCharacter.ToString(), value.PlayerName,
                    value.Points, value.NumberOfMistakes);

                leaderBoardString.AppendLine(scoreData);
            }

            if (storage.ContainsKey(LeaderBoardKey))
            {
                storage.UpdateEntry(LeaderBoardKey, leaderBoardString.ToString());
            }
            else
            {
                storage.AddEntry(LeaderBoardKey, leaderBoardString.ToString());
            }

        }

        /// <summary>
        /// adds the score to the leaderboard if the score is appropriate
        /// </summary>
        private void VerifyAndAddToBoard(PlayerScore score)
        {
            if (score.PlayerName == LeaderBoardKey)
            {
                throw new ArgumentException(string.Format("The player name may not be equal to {0}," +
                    "because the leader board data is saved to that key", LeaderBoardKey));
            }

            if (LeaderBoard.Count < LeaderBoardLength)
            {
                LeaderBoard.Add(score.Points, score);
                Task.Run((Action)SaveLeaderBoard);
            }
            else
            {
                int lastInBoard = LeaderBoard.Keys.Min();
                if (lastInBoard <= score.Points)
                {
                    LeaderBoard.Add(score.Points, score);
                    LeaderBoard.Remove(lastInBoard);
                    Task.Run((Action)SaveLeaderBoard);
                }
            }

        }

        /// <summary>
        /// Loads the leader board from the storage
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
