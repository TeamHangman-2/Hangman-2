namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Extensions;
    using Hangman.IO;
    using Resources;
    using Hangman.ScoreManagement;
    using Hangman.WordGeneration;

    /// <summary>
    /// Responsible for Running the game cycle, processing commands
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

        private void InitializeGame()
        {
            this.wordToGuess = wordGenerator.GetWord();
            this.gameIsRunning = true;
            this.ioManager.Print(GameStrings.EnterName);
            this.playerName = this.ioManager.ReadInput();
        }

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
                    this.ioManager.Print("An error occured while processing your input, Error: {0}", ex.Message);
                }
            }
        }

        private void UpdateScreen()
        {
            this.ioManager.Print(string.Empty);

            string guessesList = string.Join(", ", this.guessLog);
            this.ioManager.Print(GameStrings.NumOfWrongGuessesMade, this.WrongGuessesCount);

            if (guessesList.Length > 0)
            {
                this.ioManager.Print(GameStrings.LetterAlreadyRevealedMessage, guessesList);
            }

            this.ioManager.Print("The word to guess is:");
            this.ioManager.Print(string.Join(" ", this.wordToGuess.WordOnScreen));
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
                    throw new ArgumentOutOfRangeException("Unrecognised command!");
            }
        }

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

        private void DisplayLeaderBoard()
        {
#warning TODO: add strings to the resx file

            ioManager.Print("Leaderboard:");
            ioManager.Print("Name\tMistakesCount\tPoints");

            var leaderBoard = this.scoreManager.GetLeaderBoard();

            foreach (var item in leaderBoard)
            {
                ioManager.Print(
                    "{0}\t{1}\t{2}",
                    item.PlayerName,
                    item.NumberOfMistakes,
                    item.Points);
            }
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
