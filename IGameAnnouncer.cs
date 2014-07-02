namespace Hangman
{
    public interface IGameAnnouncer
    {
      void OutputGameStartMessage();

        void OutputGameWonMessage();

        void OutputGameLostMessage();

        // TODO: add more messages
        

        void OutputAvailableCommands();

        void OutputGuessesMade(string guessesMade);

        void OutputWordVisualisation(char[] p);

        void OutputIntroMessage();

        void OutputRepeatingGuessMessage();
    }
}
