namespace Hangman
{
    using System;
    using System.Linq;

    public class Word
    {
        private const int MaxLengthOfLetter = 1;
        private const char HiddenWordSymbol = '_';

        private string wordToGuess;
        private char[] wordOnScreen;

        public Word(string word)
        {
            this.WordToGuess = word;
            this.WordOnScreen = SetDefaultWordOnScreen(this.WordToGuess.Length);
        }

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

                if (!value.All(Char.IsLetter))
                {
                    throw new ArgumentException("Invalid word" +
                        "Word is a string that consists of letters only");
                }

                this.wordToGuess = value;
            }
        }

        public int NumberOfHiddenLetters
        {
            get
            {
                var numOfHiddenLetters = this.wordOnScreen.Count(s => s == HiddenWordSymbol);
                return numOfHiddenLetters;
            }
        }

        public bool EntireWordIsRevealed
        {
            get
            {
                var entireWordIsRevealed = !this.wordOnScreen.Contains(HiddenWordSymbol);
                return entireWordIsRevealed;
            }
        }

        public int Length
        {
            get { return this.WordToGuess.Length; }
        }

        /// <summary>
        /// This is the masked word 
        /// (some of the letters will be displayed as the hidden symbol)
        /// </summary>
        public char[] WordOnScreen
        {
            get
            {
                return this.wordOnScreen;
            }
            
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Word on screen cannot be null!");
                }

                this.wordOnScreen = value;
            }
        }

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
            if (!Char.IsLetter(currentGuess))
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

        static private char[] SetDefaultWordOnScreen(int length)
        {
            return new string(HiddenWordSymbol, length).ToCharArray();
        }

        public bool Containsletter(char letter)
        {
            if (!Char.IsLetter(letter))
            {
                throw new ArgumentException("The word consists of letters, no whitespaces, numbers or symbols");
            }

            bool result = this.WordToGuess.Contains(letter);

            return result;
        }

        #warning obsolete members, check if they are needed:

        [Obsolete("This is not used at the moment and it might not be needed anymore")]
        public void ShowLetterAt(int index)
        {
            this.WordOnScreen[index] = this.WordToGuess[index];
        }

        [Obsolete("This is not used at the moment and it might not be needed anymore")]
        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= this.WordToGuess.Length)
                {
                    throw new IndexOutOfRangeException("Incorrect index in word!");
                }

                return this.WordToGuess[index];
            }
        }

    }
}
