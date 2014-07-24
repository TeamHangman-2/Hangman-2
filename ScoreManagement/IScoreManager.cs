using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman.ScoreManagement
{
    interface IScoreManager
    {
        IList<PlayerScore> GetLeaderBoard();


        /// <summary>
        /// Saves the playerScore by updating personal record and leaderboard if needed
        /// </summary>
        void SavePlayerScore(PlayerScore score);

        PlayerScore LoadPlayerRecord(string playerName);
    }
}
