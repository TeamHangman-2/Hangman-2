namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class GameEngine
    {
        private Word wordToGuess;
        private Player player;
        private int wrongGuessesCount;
        private IList<char> letterGuesses;
        private bool gameIsRunning;
        private IGameAnnouncer announcer;
        private const int LengthOfLetter = 1;
        private const int MaxErrorsAllowed = 10;

        public GameEngine(Player player)
        {
            this.Player = player;
            this.letterGuesses = new List<char>();
            this.WrongGuessesCount = 0;
            this.announcer = new ConsoleAnnouncer();
        }

        public Player Player
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
            Console.Clear();
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
            this.announcer.OutputGameStartMessage();

            while (this.gameIsRunning)
            {
                this.UpdateScreen();
                string input = Console.ReadLine();
                this.ReadInput(input);
            }
        }

        private void UpdateScreen()
        {
            
            this.announcer.OutputAvailableCommands();
            this.announcer.OutputGuessesMade(string.Join(", ", this.letterGuesses));
            this.announcer.OutputWordVisualisation(this.wordToGuess.WordOnScreen);
        }

        public void ReadInput(string command)
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

        private void ExecuteCommand(string command)
        {
            var currCommand = this.ParseToGameCommandsEnum(command);

            switch (currCommand)
            {
                case GameCommands.Help:
                    int index = this.RevealLetter();
                    this.wordToGuess.UpdateWordOnScreen(this.wordToGuess[index]);
                    break;
                case GameCommands.Restart:
                    this.ExecuteRestartCommand();
                    break;
                case GameCommands.Exit:
                    this.ExecuteExitCommand();
                    break;
                case GameCommands.ShowResult:
                    this.ExecuteShowResultsCommand();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unrecognised command!");
            }
        }

        private GameCommands ParseToGameCommandsEnum(string command)
        {
            command = command.ToLower();

            switch (command)
            {
                case "help": return GameCommands.Help;
                case "restart": return GameCommands.Restart;
                case "exit": return GameCommands.Exit;
                case "showresults": return GameCommands.ShowResult;
                default:
                    throw new ArgumentOutOfRangeException("Unrecognised game command");
            }
        }
        public void EndGame()
        {
            this.gameIsRunning = false;
            // record player score
            if (this.wrongGuessesCount<=MaxErrorsAllowed)
            {
                this.announcer.OutputGameWonMessage(this.wrongGuessesCount);
            }
            else
            {
                this.announcer.OutputGameLostMessage(this.wrongGuessesCount);
            }
            
        }

        //private void PrintResults()
        //{
        //    int freeScoreboardPosition = 4;

        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (CommandExecuter.ScoreBoard[i] == null)
        //        {
        //            freeScoreboardPosition = i;
        //            break;
        //        }
        //    }

        //    if ((CommandExecuter.ScoreBoard[freeScoreboardPosition] == null ||
        //         mistakesCount <= CommandExecuter.ScoreBoard[freeScoreboardPosition].NumberOfMistakes)
        //          && UsedHelp == false)
        //    {
        //        Console.WriteLine("Please enter your name for the top scoreboard:");
        //        string playerName = Console.ReadLine();
        //        Player newResult = new Player(playerName, mistakesCount);
        //        CommandExecuter.ScoreBoard[freeScoreboardPosition] = newResult;

        //        for (int i = freeScoreboardPosition; i > 0; i--)
        //        {
        //            if (CommandExecuter.ScoreBoard[i].CompareTo(CommandExecuter.ScoreBoard[i - 1]) < 0)
        //            {
        //                Player betterScore = CommandExecuter.ScoreBoard[i];
        //                CommandExecuter.ScoreBoard[i] = CommandExecuter.ScoreBoard[i - 1];
        //                CommandExecuter.ScoreBoard[i - 1] = betterScore;
        //            }
        //        }
        //    }
        //}

        // Command methods from CommandExecuter.cs
        public int RevealLetter()
        {
            int result = -1;

            for (int i = 0; i < this.wordToGuess.Length; i++)
            {
                if (this.wordToGuess.WordOnScreen[i] == '_')
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        private void ExecuteRestartCommand()
        {
            this.EndGame();
            this.Start();
        }

        private void ExecuteShowResultsCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteExitCommand()
        {
            throw new NotImplementedException();
        }

        public void ProccessGuess(char currentGuess)
        {
            bool wordContainsLetter = false;

            if (this.letterGuesses.Contains(currentGuess))
            {
                this.announcer.OutputRepeatingGuessMessage();
                return;
            }

            for (int i = 0; i < this.wordToGuess.Length; i++)
            {
                var currentCharacter = this.wordToGuess[i];

                if (currentCharacter == currentGuess)
                {
                    this.wordToGuess.UpdateWordOnScreen(currentGuess);
                    wordContainsLetter = true;
                }
            }

            this.letterGuesses.Add(currentGuess);

            if (wordContainsLetter == false)
            {
                this.wrongGuessesCount++;
                // TODO: maybe add more effects

                if (this.wrongGuessesCount >= MaxErrorsAllowed)
                {
                    this.EndGame();
                    //PrintResults();
                }
            }
        }
    }
}
