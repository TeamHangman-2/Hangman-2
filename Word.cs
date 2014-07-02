namespace Hangman
{
    using System;

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

        public string WordToGuess
        {
            get { return this.wordToGuess; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Word cannot be null!");
                }

                if (value == string.Empty)
                {
                    throw new ArgumentException("Word cannot be empty string!");
                }

                this.wordToGuess = value;
            }
        }

        public int Length
        {
            get { return this.WordToGuess.Length; }
        }

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

        public char[] WordOnScreen
        {
            get { return this.wordOnScreen; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Word on screen cannot be null!");
                }

                this.wordOnScreen = value;
            }
        }

        public bool WordContainsletter(string letter)
        {
            if (letter == string.Empty)
            {
                throw new ArgumentException("Letter cannot be empty string!");
            }

            if (letter == null)
            {
                throw new ArgumentNullException("Letter cannot be null!");
            }

            if (letter.Length > MaxLengthOfLetter)
            {
                throw new ArgumentException();//TODO:need to change to more appropriate exception
            }

            bool result = this.WordToGuess.Contains(letter);

            return result;
        }

        public bool WordIsGuessed(string wordToCompare)
        {
            if (wordToCompare == this.WordToGuess)
            {
                return true;
            }

            return false;
        }

        public void UpdateWordOnScreen(char currentGuess)
        {
            if (Char.IsWhiteSpace(currentGuess))
            {
                throw new ArgumentException("Letter cannot be white space!");
            }

            if (currentGuess == null)
            {
                throw new ArgumentNullException("Letter cannot ne null!");
            }

            for (int i = 0; i < this.WordOnScreen.Length; i++)
            {
                if (this.WordToGuess[i] == currentGuess)
                {
                    this.WordOnScreen[i] = currentGuess;
                }

            }
        }

        public void ShowLetterAt(int index)
        {
            this.WordOnScreen[index] = this.WordToGuess[index];
        }

        private char[] SetDefaultWordOnScreen(int length)
        {
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = HiddenWordSymbol;
            }

            return result;
        }
    }
}
