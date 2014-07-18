using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hangman.IO
{
    public class FileStorage : IStorageProvider<string>
    {
        public Uri FilePath { get; private set; }

        private string realPath;

        public FileStorage(Uri filePath)
        {
            this.FilePath = filePath;

            if (!filePath.IsAbsoluteUri)
            {
                realPath = string.Format(@"{0}\{1}",
                    Directory.GetCurrentDirectory(), filePath.OriginalString);
            }
            else
            {
                realPath = filePath.LocalPath;
            }
        }

        public string LoadEntry(string key)
        {
            var lines = File.ReadAllLines(realPath);
            return lines.Where(x => x.StartsWith(key)).Single();        
        }

        public void UpdateKey(string key, string newValue)
        {
            var lines = File.ReadAllLines(realPath).ToList();
            var lineToUpdate = lines.Where(x => x.StartsWith(key)).Single();
            int index = lines.IndexOf(lineToUpdate);

            lines[index] = newValue;
            File.WriteAllLines(realPath, lines);
        }

        public void RemoveKey(string key)
        {
            var lines = File.ReadAllLines(realPath).ToList();
            var lineToUpdate = lines.RemoveAll(x => x.StartsWith(key));
            File.WriteAllLines(realPath, lines);
        }

        public IEnumerable<string> GetTop(int count)
        {
            var lines = File.ReadAllLines(realPath).ToList();
            return lines.Take(count);
        }
    }
}
