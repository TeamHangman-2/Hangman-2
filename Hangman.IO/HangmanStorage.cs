namespace Hangman.IO
{
    using System;
    using System.IO;

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

        /// <summary>
        /// Method that loads entry by given file name
        /// </summary>
        /// <param name="fileName">Name of file from witch load</param>
        /// <returns>All lines in file as string</returns>
        public string LoadEntry(string fileName)
        {
            string filePath = this.GenerateFilePath(fileName);
            var lines = File.ReadAllText(filePath);
            return lines;
        }

        /// <summary>
        /// Method that update entry by file nama and new value
        /// </summary>
        /// <param name="fileName">Name of file to update</param>
        /// <param name="newValue">New value of file</param>
        public void UpdateEntry(string fileName, string newValue)
        {
            string filePath = this.GenerateFilePath(fileName);
            File.WriteAllText(filePath, newValue);
        }

        /// <summary>
        /// Method that remove entry by given file name
        /// </summary>
        /// <param name="fileName">Name of file to delete</param>
        public void RemoveEntry(string fileName)
        {
            if (!this.ContainsKey(fileName))
            {
                throw new InvalidOperationException(string.Format(
                    "An entry with filename {0} does not exist", fileName));
            }

            string filePath = this.GenerateFilePath(fileName);
            File.Delete(filePath);
        }

        /// <summary>
        /// Method that add entry by file name and value
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <param name="newValue">Value of file</param>
        public void AddEntry(string fileName, string newValue)
        {
            if (this.ContainsKey(fileName))
            {
                throw new InvalidOperationException(string.Format(
                    "An entry with filename {0} already exists", fileName));
            }

            string filePath = this.GenerateFilePath(fileName);
            File.WriteAllText(filePath, newValue);
        }

        /// <summary>
        /// Method that checks if file name exist
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <returns>True if file exist and false otherwise</returns>
        public bool ContainsKey(string fileName)
        {
            string filePath = this.GenerateFilePath(fileName);
            return File.Exists(filePath);
        }

        /// <summary>
        /// Method that generate file path by given file name
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <returns>Returns generated path as string</returns>
        private string GenerateFilePath(string fileName)
        {
            string filePath = string.Format(@"{0}\{1}{2}", this.BaseDirectory, fileName, this.FileExtension);
            return filePath;
        }
    }
}
