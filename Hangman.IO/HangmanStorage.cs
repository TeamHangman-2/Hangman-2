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

        public HangmanStorage(string baseDirectory, string fileExtension = ".csv")
        {
            this.BaseDirectory = baseDirectory;
            this.FileExtension = fileExtension;
        }

        public string FileExtension { get; private set; }

        public string BaseDirectory { get; private set; }

        public string LoadEntry(string fileName)
        {
            string filePath = GenerateFilePath(fileName);
            var lines = File.ReadAllText(filePath);
            return lines;
        }

        public void UpdateEntry(string fileName, string newValue)
        {
            string filePath = GenerateFilePath(fileName);
            File.WriteAllText(filePath, newValue);
        }

        public void RemoveEntry(string fileName)
        {
            if (!this.ContainsKey(fileName))
            {
                throw new InvalidOperationException(String.Format(
                    "An entry with filename {0} does not exist", fileName));
            }

            string filePath = GenerateFilePath(fileName);
            File.Delete(filePath);
        }

        public void AddEntry(string fileName, string newValue)
        {
            if (this.ContainsKey(fileName))
            {
                throw new InvalidOperationException(String.Format(
                    "An entry with filename {0} already exists", fileName));
            }

            string filePath = GenerateFilePath(fileName);
            File.WriteAllText(filePath, newValue);
        }

        public bool ContainsKey(string fileName)
        {
            string filePath = GenerateFilePath(fileName);
            return File.Exists(filePath);
        }

        private string GenerateFilePath(string fileName)
        {
            string filePath = string.Format(@"{0}\{1}{2}", this.BaseDirectory, fileName, this.FileExtension);
            return filePath;
        }

    }
}
