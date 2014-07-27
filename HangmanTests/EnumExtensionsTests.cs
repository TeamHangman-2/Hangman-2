using Hangman.Extensions;
using Hangman;
using NUnit.Framework;
using System;
namespace HangmanTests
{
    public class EnumExtensionsTests
    {
        [TestCase("Help", GameCommands.Help)]
        [TestCase("restart", GameCommands.Restart)]
        [TestCase("exit", GameCommands.Exit)]
        [TestCase("ShowResult", GameCommands.ShowResult)]
        [TestCase("ShowCommands", GameCommands.ShowCommands)]
        public void CanCorrectlyConvertStringToEnum(string input, GameCommands result)
        {
            Assert.AreEqual(result, input.ToEnum<GameCommands>());
        }

        [Test]
        public void RejectsInvalidEnumTypes()
        {
            string justAString = "string";

            Assert.Throws<ArgumentException>(() => justAString.ToEnum<DateTime>());
        }
    }
}
