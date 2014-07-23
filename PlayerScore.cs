namespace Hangman
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
        private IStorageProvider<string, string> storage;
        private int? recordPoints;
        private int? recordMistakes;

        public PlayerScore(string playerName, IStorageProvider<string, string> storage)
        {
            this.PlayerName = playerName;
            this.storage = storage;
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

        public int RecordPoints
        {
            get
            {
                if (!this.recordPoints.HasValue)
                {
                    this.LoadRecordData();
                }

                return this.recordPoints.Value;
            }
        }

        public int RecordMistakes
        {
            get
            {
                if (!this.recordMistakes.HasValue)
                {
                    this.LoadRecordData();
                }

                return this.recordMistakes.Value;
            }
        }

        /// <summary>
        /// Loads the record for this player from a file
        /// </summary>
        public void LoadRecordData()
        {
            if (storage.ContainsKey(this.PlayerName))
            {
                var data = this.storage.LoadEntry(this.PlayerName).Split(',');
                this.recordPoints = int.Parse(data[0]);
                this.recordMistakes = int.Parse(data[1]);
            }
            else
            {
                this.recordPoints = 0;
                this.recordMistakes = 0;
            }

        }

        /// <summary>
        /// Saves the record to the file
        /// </summary>
        public void SaveRecordData()
        {
            string data = string.Join(",", this.RecordPoints, this.RecordMistakes);

            if (this.storage.ContainsKey(this.PlayerName))
            {
                this.storage.UpdateEntry(this.PlayerName, data);
            }
            else
            {
                this.storage.AddEntry(this.PlayerName, data);
            }
        }

        /// <summary>
        /// Updates the current statistics and the record if needed
        /// </summary>
        public void UpdateCurrentStats(int mistakesCount)
        {
            this.NumberOfMistakes = mistakesCount;

            if (this.points > this.recordPoints)
            {
                this.recordPoints = this.points;
            }

            if (mistakesCount < this.recordMistakes)
            {
                this.recordMistakes = mistakesCount;
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
