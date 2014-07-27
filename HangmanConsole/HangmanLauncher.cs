namespace HangmanConsole
{
    using Hangman.IO;
    using Hangman.ScoreManagement;
    using Hangman.WordGeneration;
    using Hangman;

    public class HangmanLauncher
    {
        public static void Main()
        {
            var ioManager = new ConsoleIOManager();
            var wordGenerator = new RandomWordGenerator();
            var storageManager = new HangmanStorage("");
            var scoreManager = new ScoreManager(storageManager);

            var engine = new GameEngine(ioManager, scoreManager, wordGenerator);
            engine.Start();
        }
    }
}
