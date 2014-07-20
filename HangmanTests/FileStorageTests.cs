using Hangman.IO;
using NUnit.Framework;
using System;
using System.IO;

namespace HangmanTests
{
    public class FileStorageTests
    {
        readonly string BaseFolder = Directory.GetCurrentDirectory() +
            @"\" + @"\resources";

        readonly Uri LeaderBoard = new Uri(@"\resources\FakeLeaderboard.txt", UriKind.Relative);

        [Test]
        public void CanAddEntry()
        {
            FileStorage storage = new FileStorage(LeaderBoard, BaseFolder);
            storage.AddEntry("FakePlayer2.csv", "10, 20");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer2.csv";
            Assert.True(File.Exists(outputFile));
            Assert.False(String.IsNullOrEmpty(File.ReadAllText(outputFile)));
        }

        [Test]
        public void CanRemoveEntry()
        {
            FileStorage storage = new FileStorage(LeaderBoard, BaseFolder);
            storage.RemoveEntry("FakePlayer1.txt");

            string outputFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer1.txt";
            Assert.False(File.Exists(outputFile));
        }

        [Test]
        public void CanUpdateEntry()
        {
            FileStorage storage = new FileStorage(LeaderBoard, BaseFolder);
            storage.UpdateEntry("FakePlayer1.txt", "UpdatedValue1, UpdatedValue2");

            string editedFile = Directory.GetCurrentDirectory() + @"\resources\FakePlayer1.txt";
            string newText = File.ReadAllText(editedFile);

            Assert.True(newText.Contains("UpdatedValue1"));
            Assert.True(newText.Contains("UpdatedValue2"));
        }

        [Test]
        public void CanLoadEntry()
        {
            FileStorage storage = new FileStorage(LeaderBoard, BaseFolder);
            string value = storage.LoadEntry("FakePlayer1.txt");

            Assert.False(string.IsNullOrEmpty(value));
        }

        [Test]
        public void CanCheckIfKeyExists()
        {
            FileStorage storage = new FileStorage(LeaderBoard, BaseFolder);
            bool realFileExists = storage.ContainsKey("FakePlayer1.txt");
            bool fakeFileExists = storage.ContainsKey("randomFile.txt");

            Assert.True(realFileExists);
            Assert.False(fakeFileExists);
        }

        [Test]
        public void CanLoadTop5()
        {
            FileStorage storage = new FileStorage(LeaderBoard, BaseFolder);
            var top5 = storage.GetTop(5);

            Assert.IsNotEmpty(top5);
        }
    }
}
