namespace HangmanConsole
{
    using Hangman;
    using Hangman.IO;
    using Hangman.ScoreManagement;
    using Hangman.WordGeneration;

    public class HangmanLauncher
    {
        public static void Main()
        {
            var consoleEngineBuilder = new ConsoleHangmanEngineBuilder();
            var consoleDirector = new ConsoleEngineBuilderDirector(consoleEngineBuilder);
            var gameEngine = consoleDirector.GetGameEngine();
            gameEngine.Start();
        }
    }
}
