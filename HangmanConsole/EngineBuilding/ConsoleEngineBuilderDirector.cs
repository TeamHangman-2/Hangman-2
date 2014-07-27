namespace HangmanConsole
{
    using Hangman;

    public class ConsoleEngineBuilderDirector
    {
        private ConsoleHangmanEngineBuilder builder;

        public ConsoleEngineBuilderDirector(ConsoleHangmanEngineBuilder builder)
        {
            this.builder = builder;
        }

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
