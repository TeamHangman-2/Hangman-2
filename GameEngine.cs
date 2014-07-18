namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Extensions;
    using resources;
    using Hangman.IO;


    /// <summary>
    /// Responsible for Running the game cycle, processing commands
    /// </summary>
    public class GameEngine
    {
        private const int LengthOfLetter = 1;
        private const int MaxErrorsAllowed = 10;

        private Word wordToGuess;
        private PlayerScore player;
        private int wrongGuessesCount;
        private IList<char> letterGuesses;
        private bool gameIsRunning;
        private IOManager ioManager;
        private IRecordManager recordManager;

        public GameEngine(IOManager ioManager)
        {
            this.letterGuesses = new List<char>();
            this.WrongGuessesCount = 0;
            this.ioManager = ioManager;         
        }

        public PlayerScore Player
        {
            get { return this.player; }
            private set
            {
                if (value == null)
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
                if (value < 0)
                {
                    throw new ArgumentException("Wrong guesses cannot be negative number!");
                }

                this.wrongGuessesCount = value;
            }
        }

        public void Start()
        {
#warning TODO: reset variables here instead in the constructor to assure that game restart works fine
            this.InitializeGame();
            this.RunGame();
        }

        private void InitializeGame()
        {
            this.wordToGuess = WordGenerator.GetRandomWord();
            this.gameIsRunning = true;
            // TODO: maybe read player scores from file
        }

        private void RunGame()
        {
            ioManager.Print(GameStrings.IntroductingMessage);

            while (this.gameIsRunning)
            {
                this.UpdateScreen();
                string input = ioManager.ReadInput();

                try
                {
                    this.ProcessInput(input);
                }
                catch (ArgumentException ex)
                {
                    ioManager.Print("An error occured while processing your input, Error: {0}", ex.Message);
                }

                this.Player.Points++;
            }
        }

        private void UpdateScreen()
        {
            ioManager.Print(GameStrings.AvailableCommands);
            ioManager.Print(GameStrings.Help);
            ioManager.Print(GameStrings.Restart);
            ioManager.Print(GameStrings.ShowResult);
            ioManager.Print(GameStrings.Exit);

            string guessesList = string.Join(", ", this.letterGuesses);

            ioManager.Print(GameStrings.LetterAlreadyRevealedMessage, guessesList);
            ioManager.Print(string.Join(" ", this.wordToGuess.WordOnScreen));
        }

        public void ProcessInput(string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            if (command == string.Empty)
            {
                throw new ArgumentException("Command cannot be empty string!");
            }

            if (command.Length == LengthOfLetter)
            {
                char supposedChar = command[0];
                this.ProccessGuess(supposedChar);
            }
            else
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(string commandString)
        {
            var command = commandString.ToEnum<GameCommands>();

            switch (command)
            {
                case GameCommands.Help:
                    this.wordToGuess.RevealOneLetter();
                    break;
                case GameCommands.Restart:
                    this.RestartGame();
                    break;
                case GameCommands.Exit:
                    this.ExitGame();
                    break;
                case GameCommands.ShowResult:
                    this.ShowResults();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unrecognised command!");
            }
        }

        private void EndGame()
        {
            this.gameIsRunning = false;


            if (this.wrongGuessesCount <= MaxErrorsAllowed)
            {
                ioManager.Print(GameStrings.WonMessage + " " + this.wrongGuessesCount);
            }
            else
            {
                ioManager.Print(GameStrings.LooseMessage + " " + this.wrongGuessesCount);
            }

        }

        private void RestartGame()
        {
            this.EndGame();
            this.Start();
        }

        private void ShowResults()
        {
            var leaderBoard = recordManager.LoadLeaderboard();
            throw new NotImplementedException();
        }

        private void ExitGame()
        {
            throw new NotImplementedException();
        }

        private void ProccessGuess(char currentGuess)
        {
            if (this.letterGuesses.Contains(currentGuess))
            {
                ioManager.Print(GameStrings.RepeatingGuessMessage);
                return;
            }
      
            bool wordContainsLetter = this.wordToGuess.GuessLetter(currentGuess);

            this.letterGuesses.Add(currentGuess);

            if (wordContainsLetter == false)
            {
                this.wrongGuessesCount++;
                // TODO: maybe add more effects

                if (this.wrongGuessesCount >= MaxErrorsAllowed)
                {
                    this.EndGame();
                }
            }
        }
    }
}
