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

        private readonly Uri leaderBoard = new Uri(@"\resources\FakeLeaderboard.txt", UriKind.Relative);

        [Test]
        public void CanAddEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);
            storage.AddEntry("AddedPlayer.csv", "10, 20");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\AddedPlayer.csv";
            Assert.True(File.Exists(outputFile));
            Assert.False(string.IsNullOrEmpty(File.ReadAllText(outputFile)));

            // Clean Up:
            File.Delete(outputFile);
        }

        [Test]
        public void CantAddDuplicates()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);

            Assert.Throws<InvalidOperationException>(() => storage.AddEntry("FakePlayer2.txt", "10, 20"));
        }

        [Test]
        public void CanRemoveEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);
            storage.RemoveEntry("FakePlayer1.txt");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer1.txt";
            Assert.False(File.Exists(outputFile));
        }


        [Test]
        public void CantRemoveNonExistingEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);


            Assert.Throws<InvalidOperationException>(() => storage.RemoveEntry("NonExistingFile.file"));
        }

        [Test]
        public void CanUpdateEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);
            storage.UpdateEntry("FakePlayer1.txt", "UpdatedValue1, UpdatedValue2");

            string editedFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer1.txt";
            string newText = File.ReadAllText(editedFile);

            Assert.True(newText.Contains("UpdatedValue1"));
            Assert.True(newText.Contains("UpdatedValue2"));
        }

        [Test]
        public void CanLoadEntry()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);
            string value = storage.LoadEntry("FakePlayer1.txt");

            Assert.False(string.IsNullOrEmpty(value));
        }

        [Test]
        public void CanCheckIfKeyExists()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);
            bool realFileExists = storage.ContainsKey("FakePlayer1.txt");
            bool fakeFileExists = storage.ContainsKey("randomFile.txt");

            Assert.True(realFileExists);
            Assert.False(fakeFileExists);
        }

        [Test]
        public void CanLoadTop5()
        {
            HangmanStorage storage = new HangmanStorage(this.leaderBoard, this.baseFolder);
            var top5 = storage.GetTop(5);

            Assert.IsNotEmpty(top5);
        }
    }
}
