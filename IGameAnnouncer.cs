namespace Hangman
{
    public interface IGameAnnouncer
    {
        void OutputGameStartMessage();

        void OutputGameWonMessage(int numberOfMistakes);

        void OutputGameLostMessage(int numberOfMistakes);

        // TODO: add more messages

        void OutputAvailableCommands();

        void OutputGuessesMade(string guessesMade);

        void OutputWordVisualisation(char[] word);

        void OutputRepeatingGuessMessage();
    }
}
