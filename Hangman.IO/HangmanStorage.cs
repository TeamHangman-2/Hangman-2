namespace Hangman.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Writes the entries into multiple files and the top few entries are stored in different file
    /// <para>The key is a string: the name of the file </para>
    /// </summary>
    public class HangmanStorage : IStorageProvider<string, string>
    {
        private string boardPathString;

        public HangmanStorage(Uri topEntriesFile, string baseDirectory)
        {
            this.LeaderBoardPath = topEntriesFile;
            this.BaseDirectory = baseDirectory;

            if (!topEntriesFile.IsAbsoluteUri)
            {
                this.boardPathString = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), topEntriesFile.OriginalString);
            }
            else
            {
                this.boardPathString = topEntriesFile.LocalPath;
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
            if (!this.ContainsKey(fileName))
            {
                throw new InvalidOperationException(String.Format(
                    "An entry with filename {0} does not exist", fileName));
            }

            string filePath = string.Format(@"{0}\{1}", this.BaseDirectory, fileName);
            File.Delete(filePath);
        }

        public IEnumerable<string> GetTop(int count)
        {
            var lines = File.ReadAllLines(this.boardPathString).ToList();
            return lines.Take(count);
        }

        public void SetTop(int count, ICollection<string> values)
        {
            if (values.Count != count)
            {
                throw new ArgumentException("The length of the values collection " +
                    "must be equal to the count parameter");
            }

             File.WriteAllLines(this.boardPathString, values.ToArray());
        }

        public void AddEntry(string fileName, string newValue)
        {
            if (this.ContainsKey(fileName))
            {
                throw new InvalidOperationException(String.Format(
                    "An entry with filename {0} already exists", fileName));
            }

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
