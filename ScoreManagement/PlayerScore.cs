namespace Hangman.ScoreManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Hangman.IO;

    /// <summary>
    /// A class responsible for storing player statistics and saving them into file
    /// </summary>
    public class PlayerScore : IComparable
    {
        private string playerName;
        private int numberOfMiskates;
        private int points;

        public PlayerScore(string playerName, int points, int numberOfMistakes)
        {
            this.PlayerName = playerName;
            this.NumberOfMistakes = numberOfMiskates;
            this.points = points;
          
        }

        public int Points
        {
            get
            {
                return this.points;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                this.points = value;
            }
        }

        public int NumberOfMistakes
        {
            get
            {
                return this.numberOfMiskates;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of mistakes cannot be negative number!");
                }

                this.numberOfMiskates = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return this.playerName;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Player name cannot be null!");
                }

                if (value == string.Empty)
                {
                    throw new ArgumentException("Player name cannot be empty string!");
                }

                this.playerName = value;
            }
        }

     
        public int CompareTo(object otherPlayer)
        {
            PlayerScore playerToCompareWith = otherPlayer as PlayerScore;
            int result = 0;

            if (this.NumberOfMistakes <= playerToCompareWith.NumberOfMistakes)
            {
                result = -1;
            }
            else
            {
                result = 1;
            }

            return result;
        }

        public static bool operator >=(PlayerScore first, PlayerScore second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool operator <=(PlayerScore first, PlayerScore second)
        {
            return first.CompareTo(second) <= 0;
        }
    }
}
