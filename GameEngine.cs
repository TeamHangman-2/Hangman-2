namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Extensions;



    /// <summary>
    /// Responsible for Running the game cycle, processing commands
    /// </summary>
    public class GameEngine
    {
        private const int LengthOfLetter = 1;
        private const int MaxErrorsAllowed = 10;

        private Word wordToGuess;
        private Player player;
        private int wrongGuessesCount;
        private IList<char> letterGuesses;
        private bool gameIsRunning;
        private IGameAnnouncer announcer;
        private IRecordManager recordManager;

        public GameEngine(Player player)
        {
            this.Player = player;
            this.letterGuesses = new List<char>();
            this.WrongGuessesCount = 0;
            this.announcer = new ConsoleAnnouncer();
            this.recordManager = new FileRecordManager();
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
            this.announcer.OutputGameStartMessage();

            while (this.gameIsRunning)
            {
                this.UpdateScreen();
                string input = Console.ReadLine();
                this.ProcessInput(input);
                this.Player.Points++;
            }
        }

        private void UpdateScreen()
        {
            this.announcer.OutputAvailableCommands();
            this.announcer.OutputGuessesMade(string.Join(", ", this.letterGuesses));
            this.announcer.OutputWordVisualisation(this.wordToGuess.WordOnScreen);
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

        private void LoadPlayerRecord()
        {

        }

        [Obsolete("This is not used anymore, replaced by Enum extension")]
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

        private void EndGame()
        {
            this.gameIsRunning = false;
            // record player score
            if (this.wrongGuessesCount <= MaxErrorsAllowed)
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

        //    if ((.ScoreBoard[freeScoreboardPosition] == null ||
        //         mistakesCount <= ScoreBoard[freeScoreboardPosition].NumberOfMistakes)
        //          && UsedHelp == false)
        //    {
        //        string playerName = Console.ReadLine();
        //        Player newResult = new Player(playerName, mistakesCount);
        //       ScoreBoard[freeScoreboardPosition] = newResult;

        //        for (int i = freeScoreboardPosition; i > 0; i--)
        //        {
        //            if (ScoreBoard[i].CompareTo(ScoreBoard[i - 1]) < 0)
        //            {
        //                Player betterScore = ScoreBoard[i];
        //                ScoreBoard[i] = ScoreBoard[i - 1];
        //                ScoreBoard[i - 1] = betterScore;
        //            }
        //        }
        //    }
        //}


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
                this.announcer.OutputRepeatingGuessMessage();
                return;
            }

            // This is responsbility of the word, the GameEngine should mess with Word's properties
            //for (int i = 0; i < this.wordToGuess.Length; i++)
            //{
            //    var currentCharacter = this.wordToGuess[i];

            //    if (currentCharacter == currentGuess)
            //    {
            //        this.wordToGuess.UpdateWordOnScreen(currentGuess);
            //        wordContainsLetter = true;
            //    }
            //}

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
