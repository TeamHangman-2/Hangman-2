namespace Hangman
{
    using System;

    public class Word
    {
        private const int MaxLengthOfLetter = 1;
        private const string HiddenWordSymbol = "_";
        private string word;
        private string[] wordOnScreen;

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
            }
        }

        public string[] WordOnScreen
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

        public void UpdateWordOnScreen(string letter)
        {
            if (letter==string.Empty)
            {
                throw new ArgumentException("Letter cannot be empty!");
            }

            if (letter==null)
            {
                throw new ArgumentNullException("Letter cannot ne null!");
            }

            int indexOfLetter = this.Word.IndexOf(letter);

            this.WordOnScreen[indexOfLetter] = letter;

        }

        private string[] SetDefaultWordOnScreen(int length)
        {
            string[] result = new string[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = HiddenWordSymbol;
            }

            return result;
        }
    }
}
