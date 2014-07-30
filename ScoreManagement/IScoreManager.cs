namespace Hangman.ScoreManagement
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for managing scores from players
    /// </summary>
    public interface IScoreManager
    {
        /// <summary>
        /// Get leader board
        /// </summary>
        /// <returns></returns>
        IEnumerable<PlayerScore> GetLeaderBoard();

        /// <summary>
        /// Saves the playerScore by updating personal record and leaderboard if needed
        /// </summary>
        void SavePlayerScore(PlayerScore score);

        /// <summary>
        /// Loads player's core from storage by player's name
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        PlayerScore LoadPlayerRecord(string playerName);
    }
}
