using Hangman.IO;
using System;
using System.Collections.Generic;

namespace Hangman.ScoreManagement
{
    public class ScoreManager : IScoreManager
    {
        //        private const char SeparatorSymbol = ',';

        //        public IStorageProvider<string, string> Storage { get; private set; }

        //        private IList<PlayerScore> LeaderBoard { get; set; }

        //        public IList<PlayerScore> GetLeaderBoard()
        //        {
        //            return this.LeaderBoard;
        //        }

        //        public void SavePlayerScore(PlayerScore score)
        //        {
        //#warning: TODO: check if player should be added to leader board

        //            string data = string.Join(SeparatorSymbol.ToString(), 
        //                score.Points, score.NumberOfMistakes);

        //            if (this.Storage.ContainsKey(score.PlayerName))
        //            {
        //                var recordScore = LoadPlayerRecord(score.PlayerName);
        //                if (score.Points > recordScore.Points)
        //                {
        //                    this.Storage.UpdateEntry(score.PlayerName, data);
        //                }
        //            }
        //            else
        //            {
        //                // If there is no existing record add the current score
        //                this.Storage.AddEntry(score.PlayerName, data);
        //            }
        //        }

        //        public PlayerScore LoadPlayerRecord(string playerName)
        //        {
        //            if (Storage.ContainsKey(playerName))
        //            {
        //                var data = this.Storage.LoadEntry(playerName).Split(SeparatorSymbol);
        //                int recordPoints = int.Parse(data[0]);
        //                int recordMistakes = int.Parse(data[1]);
        //                return new PlayerScore(playerName, recordPoints, recordMistakes);
        //            }
        //            else
        //            {
        //                int recordPoints = 0;
        //                int recordMistakes = 0;
        //                return new PlayerScore(playerName, recordPoints, recordMistakes);
        //            }
        //        }

        private const int LeaderBoardLength = 5;
        private const char SeparatorCharacter = ',';

        private IStorageProvider<string, string> storage;
        private SortedList<int, PlayerScore> leaderboard;

        public ScoreManager(IStorageProvider<string, string> storage)
        {
            this.storage = storage;
            this.leaderboard = new SortedList<int, PlayerScore>();
        }

        public SortedList<int, PlayerScore> GetLeaderBoard()
        {
            SortedList<int, PlayerScore> result = new SortedList<int, PlayerScore>();

            if (this.leaderboard.Count == 0)
            {
                var rawData = this.storage.GetTop(ScoreManager.LeaderBoardLength);

                foreach (var playerName in rawData)
                {
                    var currentPlayerScore = this.LoadPlayerRecord(playerName);
                    this.leaderboard.Add(currentPlayerScore.Points, currentPlayerScore);
                }
            }

            foreach (var playerScore in this.leaderboard)
            {
                result.Add(playerScore.Key, playerScore.Value);
            }

            return result;
        }

        public void SavePlayerScore(string playerName, int playerScoredPoints, int playerMistakes)
        {
            if (this.storage.ContainsKey(playerName))
            {
                var playerScore = this.LoadPlayerRecord(playerName);

                if (playerScore.Points < playerScoredPoints)
                {
                    var newPlayerData = playerScoredPoints.ToString() + playerMistakes;
                    this.storage.UpdateEntry(playerName, newPlayerData);
                }
            }
            else
            {
                var playerData = playerScoredPoints.ToString() + playerMistakes;
                this.storage.AddEntry(playerName, playerData);
            }
        }

        public PlayerScore LoadPlayerRecord(string playerName)
        {
            var rawPlayerRecord = this.storage.LoadEntry(playerName)
                                              .Split(ScoreManager.SeparatorCharacter);

            var playerPoints = int.Parse(rawPlayerRecord[0]);
            var playerMistakes = int.Parse(rawPlayerRecord[1]);

            var result = new PlayerScore(playerName, playerPoints, playerMistakes);
            return result;
        }
    }
}
