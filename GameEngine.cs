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
            this.ioManager.Print("");
            this.ioManager.Print(GameStrings.IntroductingMessage);
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
            string guessesList = string.Join(", ", this.guessLog);
            this.ioManager.Print("");
            this.ioManager.Print(GameStrings.NumOfWrongGuessesMade, this.WrongGuessesCount);

            if (guessesList.Length > 0)
            {
                this.ioManager.Print(GameStrings.LetterAlreadyRevealedMessage, guessesList);
            }

            this.ioManager.Print("The word to guess is:");
            this.ioManager.Print(string.Join(" ", this.wordToGuess.WordOnScreen));
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
                this.ExecuteCommand(command);
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
                default:
                    throw new ArgumentOutOfRangeException("Unrecognised command!");
            }
        }

        private void EndGame()
        {
            this.gameIsRunning = false;

            if (this.wrongGuessesCount <= MaxErrorsAllowed)
            {
                this.ioManager.Print(GameStrings.WinMessage + " " + this.wrongGuessesCount);
            }
            else
            {
                this.ioManager.Print(GameStrings.LossMessage + " " + this.wrongGuessesCount);
            }

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
