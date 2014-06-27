namespace Hangman
{
    using System;
    using System.Collections.Generic;

    public class GameEngine
    {
        private Word wordToGuess;
        private Player player;
        private CommandExecuter cmdExecutor;
        private int wrongGuessesCount;
        private IList<string> letterGuesses;

        public GameEngine(Player player)
        {
            this.Player = player;
            this.letterGuesses = new List<string>();
            this.WrongGuessesCount = 0;
        }

        public Player Player
        {
            get { return this.player; }
            private set
            {
                if (value==null)
                {
                    throw new ArgumentNullException("Player cannot be set to null!");
                }

                this.player = value;
            }
        }

        public int WrongGuessesCount
        {
            get { return this.wrongGuessesCount; }
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException("Wrong guesses cannot be negative number!");
                }

                this.wrongGuessesCount = value;
            }
        }

        public void Start()
        {
            this.InitializeGame();
            this.RunGame();
        }

        private void InitializeGame()
        {
            this.wordToGuess = WordGenerator.GetRandomWord();
            this.cmdExecutor = new CommandExecuter(this.wordToGuess);

            // TODO: maybe read player scores from file
        }

        private void RunGame()
        {
 	        throw new NotImplementedException();
        }

        public void ReadInput(string command)
        {
            if (command==null)
            {
                throw new ArgumentNullException();
            }

            if (command==string.Empty)
            {
                throw new ArgumentException();
            }

            switch (command)
            {
                case "Help":
                    {
                       int index= cmdExecutor.RevealLetterAt();
                       this.wordToGuess.UpdateWordOnScreen(this.wordToGuess.Word[index]);
                     }; break;

                case "Restart": ExecuteRestartCommand(); break;
                case "Exit": ExecuteExitCommand(); break;
                case "ShowResults": ExecuteShowResultsCommand(); break;
                default:
                    throw new InvalidOperationException("Invalid command!");
            }
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

        private void ExecuteShowResultsCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteExitCommand()
        {
            throw new NotImplementedException();
        }
    }
}
