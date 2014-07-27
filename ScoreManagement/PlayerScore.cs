namespace Hangman.ScoreManagement
{
    using System;

    /// <summary>
    /// A class responsible for storing player statistics and saving them into file
    /// </summary>
    public class PlayerScore : IComparable
    {
        private string playerName;
        private int numberOfMistakes;
        private int points;

        public PlayerScore(string playerName, int points, int numberOfMistakes)
        {
            this.PlayerName = playerName;
            this.NumberOfMistakes = numberOfMistakes;
            this.Points = points;
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
                return this.numberOfMistakes;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of mistakes cannot be negative number!");
                }

                this.numberOfMistakes = value;
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

        public static bool operator >=(PlayerScore first, PlayerScore second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool operator <=(PlayerScore first, PlayerScore second)
        {
            return first.CompareTo(second) <= 0;
        }
     
        public int CompareTo(object otherPlayer)
        {
            PlayerScore playerToCompareWith = otherPlayer as PlayerScore;

            if (playerToCompareWith == null)
            {
                throw new ArgumentOutOfRangeException(
                    "Passed parameter is not a PlayerScore instance.");
            }

            return this.Points.CompareTo(playerToCompareWith.Points);
        }
    }
}
