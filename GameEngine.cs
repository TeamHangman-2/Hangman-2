namespace Hangman
{
    using System;
    using System.Collections.Generic;

    public class GameEngine
    {
        private Word wordToGuess;
        private Player player;
        private CommandExecuterOld cmdExecutor;
        private int wrongGuessesCount;
        private IList<string> letterGuesses;

        public GameEngine(Player player)
        {
            this.player = player;
            this.letterGuesses = new List<string>();
        }

        public void Start()
        {
            this.InitializeGame();
            this.RunGame();
        }

        private void InitializeGame()
        {
            // TODO: create rnd word
            // TODO: create cmdExecutor
            // TODO: maybe read player scores from file
        }

        private void RunGame()
        {
 	        throw new NotImplementedException();
        }

        public void ReadInput(string command)
        {
            // this.cmdExecutor.Executecommand(string command);
            // TODO: check if input is command or guess
            // if input is command - execute command
            // else check with word
        }

        public void EndGame()
        {
            // end game messages
            // record player score
        }

        // Command methods from CommandExecuter.cs
        private void ExecuteRevealNextLetterCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteRestartCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteTopResultsCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteExitCommand()
        {
            throw new NotImplementedException();
        }
    }
}
