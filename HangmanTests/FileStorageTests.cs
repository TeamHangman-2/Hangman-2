namespace HangmanTests
{
    using System;
    using System.IO;

    using Hangman.IO;
    using NUnit.Framework;

    public class FileStorageTests
    {
        private readonly string baseFolder = Directory.GetCurrentDirectory() +
            @"\" + @"\resources";

        private readonly string existingPlayerFile = "ExistingFakePlayer";

        [Test]
        public void CanAddEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.baseFolder, ".txt");
            storage.AddEntry("AddedPlayer", "some score");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\AddedPlayer.txt";
            Assert.True(File.Exists(outputFile));
            Assert.False(string.IsNullOrEmpty(File.ReadAllText(outputFile)));

            // Clean Up:
            File.Delete(outputFile);
        }

        [Test]
        public void CantAddDuplicates()
        {
            HangmanStorage storage = new HangmanStorage(this.baseFolder);

            storage.AddEntry("FakePlayer", "some score");

            Assert.Throws<InvalidOperationException>(() => storage.AddEntry("FakePlayer", "10, 20"));

            // Clean Up:
            string outputFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer.csv";
            File.Delete(outputFile);
        }

        [Test]
        public void CanRemoveEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.baseFolder);
            storage.RemoveEntry(this.existingPlayerFile);

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\ExistingFakePlayer.csv";
            Assert.False(File.Exists(outputFile));
        }

        [Test]
        public void CantRemoveNonExistingEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.baseFolder);

            Assert.Throws<InvalidOperationException>(() => storage.RemoveEntry("NonExistingFile"));
        }

        [Test]
        public void CanUpdateEntry()
        {
            string updatedValue = "some new score that was added";

            HangmanStorage storage = new HangmanStorage(this.baseFolder);

            storage.UpdateEntry(this.existingPlayerFile, updatedValue);

            string editedFile = Directory.GetCurrentDirectory() + @"\resources\ExistingFakePlayer.csv";
            string newText = File.ReadAllText(editedFile);

            Assert.True(newText.Contains(updatedValue));
        }

        [Test]
        public void CanLoadEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.baseFolder);
            string value = storage.LoadEntry(this.existingPlayerFile);

            Assert.False(string.IsNullOrEmpty(value));
        }

        [Test]
        public void CanCheckIfKeyExists()
        {
            HangmanStorage storage = new HangmanStorage(this.baseFolder);
            bool realFileExists = storage.ContainsKey(this.existingPlayerFile);
            bool fakeFileExists = storage.ContainsKey("someNotExistingPlayer");

            Assert.True(realFileExists);
            Assert.False(fakeFileExists);
        }
    }
}
