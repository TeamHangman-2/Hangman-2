using Hangman.IO;
using System;
using System.Collections.Generic;

namespace Hangman.ScoreManagement
{
    public class ScoreManager : IScoreManager
    {
        const char SeparatorSymbol = ',';

        public IStorageProvider<string, string> Storage { get; private set; }

        private IList<PlayerScore> LeaderBoard { get; set; }

        public IList<PlayerScore> GetLeaderBoard()
        {
            return this.LeaderBoard;
        }

        public void SavePlayerScore(PlayerScore score)
        {
#warning: TODO: check if player should be added to leader board

            string data = string.Join(SeparatorSymbol.ToString(), 
                score.Points, score.NumberOfMistakes);

            if (this.Storage.ContainsKey(score.PlayerName))
            {
                var recordScore = LoadPlayerRecord(score.PlayerName);
                if (score.Points > recordScore.Points)
                {
                    this.Storage.UpdateEntry(score.PlayerName, data);
                }
            }
            else
            {
                // If there is no existing record add the current score
                this.Storage.AddEntry(score.PlayerName, data);
            }
        }

        public PlayerScore LoadPlayerRecord(string playerName)
        {
            if (Storage.ContainsKey(playerName))
            {
                var data = this.Storage.LoadEntry(playerName).Split(SeparatorSymbol);
                int recordPoints = int.Parse(data[0]);
                int recordMistakes = int.Parse(data[1]);
                return new PlayerScore(playerName, recordPoints, recordMistakes);
            }
            else
            {
                int recordPoints = 0;
                int recordMistakes = 0;
                return new PlayerScore(playerName, recordPoints, recordMistakes);
            }
        }
    }
}
