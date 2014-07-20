namespace Hangman.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileStorage : IStorageProvider<string, string>
    {
        private string boardPathString;

        public FileStorage(Uri leaderboardPath, string baseDirectory)
        {
            this.LeaderBoardPath = leaderboardPath;
            this.BaseDirectory = baseDirectory;

            if (!leaderboardPath.IsAbsoluteUri)
            {
                this.boardPathString = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), leaderboardPath.OriginalString);
            }
            else
            {
                this.boardPathString = leaderboardPath.LocalPath;
            }
        }

        public Uri LeaderBoardPath { get; private set; }

        public string BaseDirectory { get; private set; }

        public string LoadEntry(string fileName)
        {
            string filePath = string.Format(@"{0}\{1}", this.BaseDirectory, fileName);
            var lines = File.ReadAllText(filePath);
            return lines;
        }

        public void UpdateEntry(string fileName, string newValue)
        {
            string filePath = string.Format(@"{0}\{1}", this.BaseDirectory, fileName);
            File.WriteAllText(filePath, newValue);
        }

        public void RemoveEntry(string fileName)
        {
            string filePath = string.Format(@"{0}\{1}", this.BaseDirectory, fileName);
            File.Delete(filePath);
        }

        public IEnumerable<string> GetTop(int count)
        {
            var lines = File.ReadAllLines(this.boardPathString).ToList();
            return lines.Take(count);
        }

        public void AddEntry(string fileName, string newValue)
        {
            string filePath = string.Format(@"{0}\{1}", this.BaseDirectory, fileName);
            File.WriteAllText(filePath, newValue);
        }

        public bool ContainsKey(string fileName)
        {
            string filePath = string.Format(@"{0}\{1}", this.BaseDirectory, fileName);
            return File.Exists(filePath);
        }
    }
}
