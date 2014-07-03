namespace Hangman
{
    using System.Collections.Generic;

    public interface IRecordManager
    {
        void SavePlayerResults(string playerName, int playerScore, string savePath) ;

        SortedSet<int> LoadPlayerResults(string playerName, string loadPath);

        SortedSet<Player> LoadLeaderboard();
    }
}
