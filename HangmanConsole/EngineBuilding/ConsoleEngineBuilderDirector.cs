namespace HangmanConsole
{
    using Hangman;

    /// <summary>
    /// Class that direct building of game engine
    /// </summary>
    public class ConsoleEngineBuilderDirector
    {
        private ConsoleHangmanEngineBuilder builder;

        public ConsoleEngineBuilderDirector(ConsoleHangmanEngineBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Method that creates game engine
        /// </summary>
        /// <returns>Return created game engine</returns>
        public GameEngine GetGameEngine()
        {
            this.builder.AddIoManager();
            this.builder.AddWordGenerator();
            this.builder.AddStorageProvider();
            this.builder.AddScoreManager();
            var gameEngine = this.builder.BuildGameEngine();

            return gameEngine;
        }
    }
}
