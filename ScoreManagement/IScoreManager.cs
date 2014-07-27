namespace Hangman.ScoreManagement
{
    using System.Collections.Generic;

    public interface IScoreManager
    {
        IEnumerable<PlayerScore> GetLeaderBoard();

        /// <summary>
        /// Saves the playerScore by updating personal record and leaderboard if needed
        /// </summary>
        void SavePlayerScore(PlayerScore score);

        PlayerScore LoadPlayerRecord(string playerName);
    }
}
