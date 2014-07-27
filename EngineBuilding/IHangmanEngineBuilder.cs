namespace Hangman.EngineBuilding
{
    using Hangman.IO;
    using Hangman.ScoreManagement;
    using Hangman.WordGeneration;

    public interface IHangmanEngineBuilder
    {
        void AddIoManager();

        void AddWordGenerator();

        void AddScoreManager();

        GameEngine BuildGameEngine();
    }
}
