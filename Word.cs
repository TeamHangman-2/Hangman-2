namespace Hangman
{
    using System;

    public class Word
    {
        private const int MaxLengthOfLetter = 1;
        private string word;
        private char[] wordOnScreen;

        public Word(string word)
        {
            this.Word = word;
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
            throw new NotImplementedException();
        }
    }
}
