﻿namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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
        private const int EntireWordGuessedBonus = 2;

        private Word wordToGuess;
        private PlayerScore player;
        private ISet<char> guessLog;
        private bool gameIsRunning;
        private IOManager ioManager;
        private IStorageProvider<string, string> dataStorage;

        private int wrongGuessesCount;
        private int points;

        public GameEngine(IOManager ioManager)
        {
            this.guessLog = new SortedSet<char>();
            this.WrongGuessesCount = 0;
            this.ioManager = ioManager;
        }

        public PlayerScore Player
        {
            get
            {
                return this.player;
            }

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
            get
            {
                return this.wrongGuessesCount;
            }

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
#warning TODO: reset variables here instead of in the constructor to assure that game restart works fine
            this.InitializeGame();
            this.RunGame();
        }

        private void InitializeGame()
        {
            this.wordToGuess = WordGenerator.GetRandomWord();
            this.gameIsRunning = true;

            // read player name:
            this.ioManager.Print(GameStrings.EnterName);
            string playerName = this.ioManager.ReadInput();
            // create a storage and player instance
            this.Player = new PlayerScore(playerName, this.dataStorage);
        }

        private void RunGame()
        {
            this.ioManager.ClearOutputWindow();
            this.ioManager.Print(GameStrings.IntroductingMessage);
            this.ioManager.Print(GameStrings.MaxWrongGuessesWarningMessage, MaxErrorsAllowed);
            this.PrintCommands();

            while (this.gameIsRunning)
            {
                this.UpdateScreen();

                string input = this.ioManager.ReadInput();

                try
                {
                    this.ProcessInput(input);
                }
                catch (ArgumentException ex)
                {
                    this.ioManager.Print("An error occured while processing your input, Error: {0}", ex.Message);
                }
            }

        }

        private void UpdateScreen()
        {
            this.ioManager.Print("");

            string guessesList = string.Join(", ", this.guessLog);
            this.ioManager.Print(GameStrings.NumOfWrongGuessesMade, this.WrongGuessesCount);

            if (guessesList.Length > 0)
            {
                this.ioManager.Print(GameStrings.LetterAlreadyRevealedMessage, guessesList);
            }

            this.ioManager.Print("The word to guess is:");
            this.ioManager.Print(string.Join(" ", this.wordToGuess.WordOnScreen));
        }

        public void ProcessInput(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            if (input == string.Empty)
            {
                throw new ArgumentException("Command cannot be empty string!");
            }

            input = input.ToLower();

            if (input.Length == LengthOfLetter)
            {
                char supposedChar = input[0];
                this.ProccessGuess(supposedChar);
            }
            else if (input[0] == '-')
            {
                input = input.Substring(1);
                this.ExecuteCommand(input);
            }
            else
            {
                this.AttemptToGuessEntireWord(input);
            }
        }

        private void AttemptToGuessEntireWord(string wordGuess)
        {
            if (wordGuess.Length != this.wordToGuess.Length)
            {
                throw new ArgumentException("Guess and word are of different lengths.");
            }

            var wordIsGuessed = this.wordToGuess.GuessWholeWord(wordGuess);

            if (wordIsGuessed)
            {
                var hiddenLettersCount = this.wordToGuess.NumberOfHiddenLetters;

                if (hiddenLettersCount >= this.wordToGuess.Length / 2)
                {
                    this.player.Points += hiddenLettersCount * EntireWordGuessedBonus;
                }

                this.EndGame();
            }
            else
            {
                this.wrongGuessesCount = MaxErrorsAllowed + 1;
                this.EndGame();
            }
        }

        private void PrintCommands()
        {
            this.ioManager.Print("");
            this.ioManager.Print(GameStrings.AvailableCommands);
            this.ioManager.Print("  -" + GameStrings.Help);
            this.ioManager.Print("  -" + GameStrings.Restart);
            this.ioManager.Print("  -" + GameStrings.ShowResult);
            this.ioManager.Print("  -" + GameStrings.Exit);
            this.ioManager.Print("  -" + GameStrings.ShowCommands);
            this.ioManager.Print("");
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
                case GameCommands.ShowCommands:
                    this.PrintCommands();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unrecognised command!");
            }
        }

        private void EndGame()
        {
            this.gameIsRunning = false;
            this.ioManager.Print("");

            if (this.wrongGuessesCount <= MaxErrorsAllowed)
            {
                this.ioManager.Print(GameStrings.WinMessage + " " + this.wrongGuessesCount);
            }
            else
            {
                this.ioManager.Print(GameStrings.LossMessage + " " + this.wrongGuessesCount);
            }

            this.ioManager.Print(GameStrings.YouScoredPtsMsg, this.player.Points);

            this.Player.UpdateCurrentStats(this.wrongGuessesCount);
            this.Player.SaveRecordData();
        }

        private void RestartGame()
        {
            this.EndGame();
            this.Start();
        }

        private void ShowResults()
        {
            throw new NotImplementedException();
        }

        private void ExitGame()
        {
            throw new NotImplementedException();
        }

        private void ProccessGuess(char currentGuess)
        {
            if (this.guessLog.Contains(currentGuess))
            {
                this.ioManager.Print(GameStrings.RepeatingGuessMessage, currentGuess);
                return;
            }

            bool wordContainsLetter = this.wordToGuess.Containsletter(currentGuess);
            this.guessLog.Add(currentGuess);

            if (wordContainsLetter)
            {
                this.wordToGuess.UpdateWordOnScreen(currentGuess);
                this.Player.Points++;
            }
            else
            {
                // TODO: maybe add more effects
                this.wrongGuessesCount++;
            }

            if (this.wordToGuess.EntireWordIsRevealed ||
                this.wrongGuessesCount > MaxErrorsAllowed)
            {
                this.EndGame();
            }
        }
    }
}
