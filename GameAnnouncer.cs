namespace Hangman
{
    public interface GameAnnouncer
    {
        void OutputGameStartMessage();

        void OutputGameWonMessage();

        void OutputGameLostMessage();

        // TODO: add more messages
    }
}
