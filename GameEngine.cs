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
        private IList<char> letterGuesses;
        private bool gameIsRunning;
        private const int LengthOfLetter = 1;
        private const int MaxErrorsAllowed = 10;

        public GameEngine(Player player)
        {
            this.Player = player;
            this.letterGuesses = new List<char>();
            this.WrongGuessesCount = 0;
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
            this.InitializeGame();
            this.RunGame();
        }

        private void InitializeGame()
        {
            this.wordToGuess = WordGenerator.GetRandomWord();
            this.cmdExecutor = new CommandExecuter(this.wordToGuess);
            this.gameIsRunning = true;
            // TODO: maybe read player scores from file
        }

        private void RunGame()
        {
            throw new NotImplementedException();
        }

        public void ReadInput(string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            if (command == string.Empty)
            {
                throw new ArgumentException();
            }

            if (command.Length < LengthOfLetter)
            {
                char supposedChar = command[0];
                //GameManager.ProccessGuess(this.wordToGuess, supposedChar);
            }
            else
            {
                ExecuteCommand(command);
            }
            // this.cmdExecutor.Executecommand(string command);
            // TODO: check if input is command or guess
            // if input is command - execute command
            // else check with word
        }

        private void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "Help":
                    {
                        int index = cmdExecutor.RevealLetter();
                        this.wordToGuess.UpdateWordOnScreen(this.wordToGuess[index]);
                    } break;

                case "Restart": ExecuteRestartCommand(); break;
                case "Exit": ExecuteExitCommand(); break;
                case "ShowResults": ExecuteShowResultsCommand(); break;
                default:
                    throw new InvalidOperationException("Invalid command!");
            }
        }

        public void EndGame()
        {
            // end game messages
            // record player score

            Console.WriteLine("You won with {0} mistakes.", mistakesCount);
            RevealGuessedLetters(word);
            int freeScoreboardPosition = 4;

            for (int i = 0; i < 4; i++)
            {
                if (CommandExecuter.ScoreBoard[i] == null)
                {
                    freeScoreboardPosition = i;
                    break;
                }
            }

            if ((CommandExecuter.ScoreBoard[freeScoreboardPosition] == null ||
                 mistakesCount <= CommandExecuter.ScoreBoard[freeScoreboardPosition].NumberOfMistakes)
                  && UsedHelp == false)
            {
                Console.WriteLine("Please enter your name for the top scoreboard:");
                string playerName = Console.ReadLine();
                Player newResult = new Player(playerName, mistakesCount);
                CommandExecuter.ScoreBoard[freeScoreboardPosition] = newResult;

                for (int i = freeScoreboardPosition; i > 0; i--)
                {
                    if (CommandExecuter.ScoreBoard[i].CompareTo(CommandExecuter.ScoreBoard[i - 1]) < 0)
                    {
                        Player betterScore = CommandExecuter.ScoreBoard[i];
                        CommandExecuter.ScoreBoard[i] = CommandExecuter.ScoreBoard[i - 1];
                        CommandExecuter.ScoreBoard[i - 1] = betterScore;
                    }
                }
            }

            RevealedCount = 0;
            mistakesCount = 0;
            UsedHelp = false;
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

        public void ProccessGuess(Word word, char currentGuess)
        {
            bool wordContainsLetter = false;

            if (this.letterGuesses.Contains(currentGuess))
            {
                //Console.WriteLine("You have already revelaed the letter {0}", charSupposed);
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
                }
            }
        }
    }
}
