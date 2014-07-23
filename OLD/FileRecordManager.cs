namespace Hangman
{
    using System.Collections.Generic;
    using System.IO;

    public class FileRecordManager : IRecordManager
    {
        public void SavePlayerResults(string playerName, int playerScore, string savePath = "./data/")
        {
            savePath += playerName + "_results";
            var writer = new StreamWriter(savePath, true);

            using (writer)
            {
                writer.WriteLine(playerScore);
            }
        }

        public SortedSet<int> LoadPlayerResults(string playerName, string loadPath = "./data/")
        {
            var result = new SortedSet<int>();
            var reader = new StreamReader(loadPath);

            using (reader)
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    var currScore = int.Parse(line);
                    result.Add(currScore);
                }
            }

            return result;
        }

        public SortedSet<PlayerScore> LoadLeaderboard()
        {
            throw new System.NotImplementedException();
        }
    }
}
