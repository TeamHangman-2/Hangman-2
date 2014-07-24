using Hangman.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    public class LeaderBoard
    {
        public int TopCount { get; private set; }

        public IStorageProvider<string, string> Storage { get; private set; }

        SortedSet<PlayerScore> topPlayers;

        public SortedSet<PlayerScore> TopPlayers
        {
            get
            {
                if (topPlayers == null)
                {
                    LoadScoreBoard();
                }
                return topPlayers;
            }
        }

        public LeaderBoard(IStorageProvider<string, string> storage, int topCount = 5)
        {
            this.Storage = storage;
            this.TopCount = topCount;
        }

        /// <summary>
        /// Adds the score in the TopPlayers if it's between
        /// </summary>
        /// <param name="score"></param>
        public void CommitPlayerScore(PlayerScore score)
        {
            if (TopPlayers.Min <= score && score <= TopPlayers.Max)
            {
                TopPlayers.Add(score);
                TopPlayers.Remove(TopPlayers.Max);
                SaveScoreBoard();
            }
        }

        private void LoadScoreBoard()
        {
            this.topPlayers = new SortedSet<PlayerScore>();
            var playersData = Storage.GetTop(TopCount);

            foreach (var player in playersData)
            {
                var playerScore = new PlayerScore(player, Storage);
                this.topPlayers.Add(playerScore);
            }
        }

        private void SaveScoreBoard()
        {
            var topPlayersNames = TopPlayers.Select(x=> x.PlayerName).ToList();
            (Storage as HangmanStorage).SetTop(TopCount, topPlayersNames);
        }
    }
}
