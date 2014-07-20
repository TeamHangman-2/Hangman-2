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
            FileStorage storage = new FileStorage(this.leaderBoard, this.baseFolder);
            storage.AddEntry("FakePlayer2.csv", "10, 20");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer2.csv";
            Assert.True(File.Exists(outputFile));
            Assert.False(string.IsNullOrEmpty(File.ReadAllText(outputFile)));
        }

        [Test]
        public void CanRemoveEntry()
        {
            FileStorage storage = new FileStorage(this.leaderBoard, this.baseFolder);
            storage.RemoveEntry("FakePlayer1.txt");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer1.txt";
            Assert.False(File.Exists(outputFile));
        }

        [Test]
        public void CanUpdateEntry()
        {
            FileStorage storage = new FileStorage(this.leaderBoard, this.baseFolder);
            storage.UpdateEntry("FakePlayer1.txt", "UpdatedValue1, UpdatedValue2");

            string editedFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer1.txt";
            string newText = File.ReadAllText(editedFile);

            Assert.True(newText.Contains("UpdatedValue1"));
            Assert.True(newText.Contains("UpdatedValue2"));
        }

        [Test]
        public void CanLoadEntry()
        {
            FileStorage storage = new FileStorage(this.leaderBoard, this.baseFolder);
            string value = storage.LoadEntry("FakePlayer1.txt");

            Assert.False(string.IsNullOrEmpty(value));
        }

        [Test]
        public void CanCheckIfKeyExists()
        {
            FileStorage storage = new FileStorage(this.leaderBoard, this.baseFolder);
            bool realFileExists = storage.ContainsKey("FakePlayer1.txt");
            bool fakeFileExists = storage.ContainsKey("randomFile.txt");

            Assert.True(realFileExists);
            Assert.False(fakeFileExists);
        }

        [Test]
        public void CanLoadTop5()
        {
            FileStorage storage = new FileStorage(this.leaderBoard, this.baseFolder);
            var top5 = storage.GetTop(5);

            Assert.IsNotEmpty(top5);
        }
    }
}
