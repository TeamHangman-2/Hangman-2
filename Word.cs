namespace Hangman
{
    using System;

    public class Word
    {
        private const int MaxLengthOfLetter = 1;
        private const char HiddenWordSymbol = '_';
        private string word;
        private char[] wordOnScreen;

        public Word(string word)
        {
            this.Word = word;
            this.WordOnScreen = SetDefaultWordOnScreen(this.Word.Length);
        }

        public string Word
        {
            get { return this.word; }
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

                this.word = value;
            }
        }

        public int Length
        {
            get { return this.Word.Length; }
        }

        public char this[int index]
        {
            get 
            {
                if (index<0 || index>=this.Word.Length)
                {
                    throw new IndexOutOfRangeException("Incorrect index in word!");
                }

                return this.Word[index]; 
            }
            set
            {
                if (index < 0 || index >= this.Word.Length)
                {
                    throw new IndexOutOfRangeException("Incorrect index in word!");
                }

                this.Word[index] = value;
            }
        }

        public char[] WordOnScreen
        {
            get { return this.wordOnScreen; }
            private set
            {
                if (value==null)
                {
                    throw new ArgumentNullException("Word on screen cannot be null!");
                }

                this.wordOnScreen = value;
            }
        }

        public bool WordContainsletter(string letter)
        {
            if (letter==string.Empty)
            {
                throw new ArgumentException("Letter cannot be empty string!");
            }

            if (letter==null)
            {
                throw new ArgumentNullException("Letter cannot be null!");
            }

            if (letter.Length>MaxLengthOfLetter)
            {
                throw new ArgumentException();//TODO:need to change to more appropriate exception
            }

            bool result = this.Word.Contains(letter);

            return result;
        }

        public bool WordIsGuessed(string wordToCompare)
        {
            if (wordToCompare == this.Word)
            {
                return true;
            }

            return false;
        }

        public void UpdateWordOnScreen(char letter)
        {
            if (letter==string.Empty)
            {
                throw new ArgumentException("Letter cannot be empty!");
            }

            if (letter==null)
            {
                throw new ArgumentNullException("Letter cannot ne null!");
            }

            for (int i = 0; i < this.WordOnScreen.Length; i++)
            {
                if (this.Word[i]==letter)
                {
                    this.WordOnScreen[i] = letter;
                }
               
            }
        }

        public void ShowLetterAt(int index)
        {
            this.WordOnScreen[index] = this.Word[index];
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
