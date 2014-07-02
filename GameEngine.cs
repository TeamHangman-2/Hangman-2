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

        public static void ProccessGuess(Word word, char charSupposed)
        {
            bool UsedHelp = false;
            int RevealedCount = 0;
            //char[] GuessedLetters;//= new int[this.Word.Length];
            
            //StringBuilder wordInitailized = new StringBuilder();
            int letterApperance = 0;

            if (true)
            {

                //Console.WriteLine("You have already revelaed the letter {0}", charSupposed);
            }
            else
            {
                //for (int i = 0; i < word.Length; i++)
                //{
                //    if (word[i].Equals(charSupposed))
                //    {
                //        GuessedLetters[i] = word[i];
                //        letterApperance++;
                //    }
                //}

                //if (letterApperance == 0)
                //{
                //    Console.WriteLine("Sorry! There are no unrevealed letters {0}", charSupposed);
                //    mistakesCount++;
                //}
                //else
                //{
                //    Console.WriteLine("Good job! You revealed {0} letters.", letterApperance);
                //    RevealedCount += letterApperance;
                //}

                //if (RevealedCount == word.Length)
                //{
                //    FinalizeGame(word);
                //    CommandExecuter.Restart();
                //}

                //Console.WriteLine("The secret word is:");
                //RevealGuessedLetters(word);
            }
        }
    }
}
