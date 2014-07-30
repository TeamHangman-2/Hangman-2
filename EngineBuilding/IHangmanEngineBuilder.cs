namespace Hangman.EngineBuilding
{
    /// <summary>
    /// Interface for building engine
    /// </summary>
    public interface IHangmanEngineBuilder
    {
        void AddIoManager();

        void AddWordGenerator();

        void AddScoreManager();

        GameEngine BuildGameEngine();
    }
}
