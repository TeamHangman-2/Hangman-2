﻿namespace Hangman
{
    using System;
    using System.Collections.Generic;

    using Extensions;
    using Hangman.IO;
    using Hangman.ScoreManagement;
    using Hangman.WordGeneration;
    using Resources;
    using System.Text;

    /// <summary>
    /// Class that manages the game cycle:initialize game, start game,
    /// restart game, end game and processes input
    /// </summary>
    public class GameEngine
    {
        private const int LengthOfLetter = 1;
        private const int MaxErrorsAllowed = 10;
        private const int EntireWordGuessedBonus = 2;

        private bool gameIsRunning;
        private Word wordToGuess;
        private ISet<char> guessLog;
        private IOManager ioManager;
        private IWordGenerator wordGenerator;
        private IScoreManager scoreManager;
        private int wrongGuessesCount;
        private string playerName;
        private int playerScore;

        public GameEngine(IOManager ioManager, IScoreManager scoreManager, IWordGenerator wordGenerator)
        {
            this.guessLog = new SortedSet<char>();
            this.WrongGuessesCount = 0;
            this.playerScore = 0;
            this.ioManager = ioManager;
            this.scoreManager = scoreManager;
            this.wordGenerator = wordGenerator;
        }

        /// <summary>
        /// Property that gets and sets number of wrong guesses
        /// </summary>
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
                    throw new ArgumentException(
                        ExceptionMessages.NumOfWrongGuessesCannotBeNegative);
                }

                this.wrongGuessesCount = value;
            }
        }

        /// <summary>
        /// Method that starts game
        /// </summary>
        public void Start()
        {
//// #warning TODO: reset variables here instead of in the constructor to assure that game restart works fine
            this.InitializeGame();
            this.RunGame();
        }

        /// <summary>
        /// Mathod that handle input. Define input as command or guessed letter or attempt to guess whole word
        /// </summary>
        public void ProcessInput(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(ExceptionMessages.InputNull);
            }

            if (input == string.Empty)
            {
                throw new ArgumentException(ExceptionMessages.InputFormatError);
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

        /// <summary>
        ///Method that initializes all nessesary fields to start game
        /// </summary>
        private void InitializeGame()
        {
            this.wordToGuess = this.wordGenerator.GetWord();
            this.gameIsRunning = true;
            this.ioManager.Print(GameStrings.EnterName);
            this.playerName = this.ioManager.ReadInput();
        }

        /// <summary>
        /// Method that manages game cycle 
        /// </summary>
        private void RunGame()
        {
            this.ioManager.ClearOutputWindow();
            this.ioManager.Print(GameStrings.IntroductingMessage);
            this.ioManager.Print(GameStrings.MaxWrongGuessesWarningMessage, MaxErrorsAllowed);
            this.PrintCommandsList();

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
                    this.ioManager.Print(ExceptionMessages.InputProcessingError, ex.Message);
                }
            }
        }

        /// <summary>
        /// Mathod that updates screen with number of wrong guesses, guesses letter and word on screen
        /// </summary>
        private void UpdateScreen()
        {
            this.ioManager.Print(string.Empty);

            string guessesList = string.Join(", ", this.guessLog);
            this.ioManager.Print(GameStrings.NumOfWrongGuessesMade, this.WrongGuessesCount);

            if (guessesList.Length > 0)
            {
                this.ioManager.Print(GameStrings.LetterAlreadyRevealedMessage, guessesList);
            }

            this.ioManager.Print(GameStrings.GuesswordIntroductionMsg);
            this.ioManager.Print(string.Join(" ", this.wordToGuess.WordOnScreen));
        }

        /// <summary>
        /// Method that attempts to guess entire word by word as string.
        /// If word is guessed player gets bonus points, otherwise
        /// wrong guesses becomes maximal allowed.Then game ends. 
        /// </summary>
        /// <param name="wordGuess">Guessed word</param>
        private void AttemptToGuessEntireWord(string wordGuess)
        {
            if (wordGuess.Length != this.wordToGuess.Length)
            {
                throw new ArgumentException(ExceptionMessages.GuessAndWordHaveDiffLengths);
            }

            var wordIsGuessed = this.wordToGuess.GuessWholeWord(wordGuess);

            if (wordIsGuessed)
            {
                var hiddenLettersCount = this.wordToGuess.NumberOfHiddenLetters;

                if (hiddenLettersCount >= this.wordToGuess.Length / 2)
                {
                    this.playerScore += hiddenLettersCount * EntireWordGuessedBonus;
                }

                this.EndGame();
            }
            else
            {
                this.wrongGuessesCount = MaxErrorsAllowed + 1;
                this.EndGame();
            }
        }

        /// <summary>
        /// Method that prints all available commands in game
        /// </summary>
        private void PrintCommandsList()
        {
            this.ioManager.Print(string.Empty);
            this.ioManager.Print(GameStrings.AvailableCommands);
            this.ioManager.Print("  -" + GameStrings.Help);
            this.ioManager.Print("  -" + GameStrings.Restart);
            this.ioManager.Print("  -" + GameStrings.ShowResult);
            this.ioManager.Print("  -" + GameStrings.Exit);
            this.ioManager.Print("  -" + GameStrings.ShowCommands);
            this.ioManager.Print(string.Empty);
        }

        /// <summary>
        /// Method that executes command by given string as command
        /// </summary>
        /// <param name="commandString">Command to execute</param>
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
                    this.DisplayLeaderBoard();
                    break;
                case GameCommands.ShowCommands:
                    this.PrintCommandsList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(ExceptionMessages.UnrecognisedCommand);
            }
        }

        /// <summary>
        /// Method that ends game 
        /// </summary>
        private void EndGame()
        {
            this.gameIsRunning = false;
            this.ioManager.Print(string.Empty);

            if (this.wrongGuessesCount <= MaxErrorsAllowed)
            {
                this.ioManager.Print(GameStrings.WinMessage + " " + this.wrongGuessesCount);
            }
            else
            {
                this.ioManager.Print(GameStrings.LossMessage + " " + this.wrongGuessesCount);
            }

            this.ioManager.Print(GameStrings.YouScoredPtsMsg, this.playerScore);

            var score = new PlayerScore(this.playerName, this.playerScore, this.wrongGuessesCount);

            this.scoreManager.SavePlayerScore(score);
        }

        private void RestartGame()
        {
            this.EndGame();
            this.Start();
        }

        /// <summary>
        /// Method that displays leader board 
        /// </summary>
        private void DisplayLeaderBoard()
        {
            this.ioManager.Print(string.Empty);
            this.ioManager.Print(GameStrings.LeaderboardTitle);
            this.ioManager.Print(GameStrings.LeaderboardTableTitle.Replace("\\t", "\t"));

            var leaderBoard = this.scoreManager.GetLeaderBoard();
            var result = string.Empty;

            foreach (var item in leaderBoard)
            {
                result = string.Format(
                    GameStrings.LeaderboardTableBodyFormatter.Replace("\\t", "\t"),
                    item.PlayerName,
                    item.Points,
                    item.NumberOfMistakes) + Environment.NewLine + result;
            }

            this.ioManager.Print(result);
        }

        private void ExitGame()
        {
            Environment.Exit(0);
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
                this.playerScore++;
            }
            else
            {
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
