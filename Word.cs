namespace Hangman
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represent word in game. Contains word as private string field and array of chars
    /// represent guessed characters.
    /// </summary>
    public class Word
    {
        private const int MaxLengthOfLetter = 1;
        private const char HiddenWordSymbol = '_';

        private string wordToGuess;

        public Word(string word)
        {
            this.WordToGuess = word;
            this.WordOnScreen = SetDefaultWordOnScreen(this.WordToGuess.Length);
        }

        /// <summary>
        /// Property that get number of hidden letters in word to guess
        /// </summary>
        public int NumberOfHiddenLetters
        {
            get
            {
                var numOfHiddenLetters = this.WordOnScreen.Count(s => s == HiddenWordSymbol);
                return numOfHiddenLetters;
            }
        }

        /// <summary>
        /// Property that check if entire word is guessed
        /// </summary>
        public bool EntireWordIsRevealed
        {
            get
            {
                var entireWordIsRevealed = !this.WordOnScreen.Contains(HiddenWordSymbol);
                return entireWordIsRevealed;
            }
        }

        /// <summary>
        /// Property that get length of word to guess
        /// </summary>
        public int Length
        {
            get { return this.WordToGuess.Length; }
        }

        /// <summary>
        /// This is the masked word 
        /// (some of the letters will be displayed as the "hidden symbol")
        /// </summary>
        public char[] WordOnScreen { get; private set; }

        /// <summary>
        /// Property that get and set word to guess
        /// </summary>
        private string WordToGuess
        {
            get
            {
                return this.wordToGuess;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Word cannot be null!");
                }

                if (value == string.Empty)
                {
                    throw new ArgumentException("Word cannot be empty string!");
                }

                if (!value.All(char.IsLetter))
                {
                    throw new ArgumentException("Invalid word" +
                        "Word is a string that consists of letters only");
                }

                this.wordToGuess = value;
            }
        }

        /// <summary>
        /// Method that check if whole word is guessed for single attempt.
        /// </summary>
        /// <param name="wordToCompare">Guessed word</param>
        /// <returns>Return true if word is guessed and false otherwise</returns>
        public bool GuessWholeWord(string wordToCompare)
        {
            var result = wordToCompare == this.WordToGuess;
            return result;
        }

        /// <summary>
        /// If the word contains the letter, it will be revealed on every position.
        /// <para> Returns true if any letter was guessed </para>
        /// </summary>
        /// <param name="currentGuess">the letter to be guessed</param>
        public void UpdateWordOnScreen(char currentGuess)
        {
            if (!char.IsLetter(currentGuess))
            {
                throw new ArgumentException("The word consists of letters, no whitespaces, numbers or symbols");
            }

            for (int i = 0; i < this.WordOnScreen.Length; i++)
            {
                if (this.WordToGuess[i] == currentGuess)
                {
                    this.WordOnScreen[i] = currentGuess;
                }
            }
        }

        /// <summary>
        /// Method that reveal one letter in word on screen when help command
        /// is write
        /// </summary>
        public void RevealOneLetter()
        {
            for (int i = 0; i < this.WordOnScreen.Length; i++)
            {
                if (this.WordOnScreen[i] == HiddenWordSymbol)
                {
                    this.WordOnScreen[i] = this.wordToGuess[i];
                    break;
                }
            }
        }

        /// <summary>
        /// Method that check if given letter is contained in word to guess.
        /// </summary>
        /// <param name="letter">Character to check</param>
        /// <returns>True if word contains letter and false otherwise</returns>
        public bool Containsletter(char letter)
        {
            if (!char.IsLetter(letter))
            {
                throw new ArgumentException("The word consists of letters, no whitespaces, numbers or symbols");
            }

            bool result = this.WordToGuess.Contains(letter);

            return result;
        }

        /// <summary>
        /// Set default word on sreen by length of word
        /// </summary>
        /// <param name="length">Length of default word</param>
        /// <returns>Char array represents word on screen</returns>
        private static char[] SetDefaultWordOnScreen(int length)
        {
            return new string(HiddenWordSymbol, length).ToCharArray();
        }
    }
}
