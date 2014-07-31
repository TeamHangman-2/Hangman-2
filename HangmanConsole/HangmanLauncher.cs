namespace HangmanConsole
{
    /// <summary>
    /// Class that contains main method. Build game engine and starts game
    /// </summary>
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
