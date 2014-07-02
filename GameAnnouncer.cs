namespace Hangman
{
    public interface GameAnnouncer
    {
        void OutputGameStartMessage();

        void OutputGameWonMessage();

        void OutputGameLostMessage();

        // TODO: add more messages
        //"You have already revelaed the letter"
        //private const string IntroductingMessage = "Welcome to “Hangman” game. Please try to guess my secret word.\nUse 'top' to view the top scoreboard, 'restart' to start a new game,'help' to cheat and 'exit' to quit the game.";
    }
}
