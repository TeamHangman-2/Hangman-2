namespace Hangman.EngineBuilding
{
    public interface IHangmanEngineBuilder
    {
        void AddIoManager();

        void AddWordGenerator();

        void AddScoreManager();

        GameEngine BuildGameEngine();
    }
}
