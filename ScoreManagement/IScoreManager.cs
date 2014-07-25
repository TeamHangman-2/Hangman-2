using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.ScoreManagement
{
    public interface IScoreManager
    {
        SortedList<int, PlayerScore> GetLeaderBoard();

        /// <summary>
        /// Saves the playerScore by updating personal record and leaderboard if needed
        /// </summary>
        void SavePlayerScore(string playerName, int playerScore, int numberOfMistakes);

        PlayerScore LoadPlayerRecord(string playerName);
    }
}
