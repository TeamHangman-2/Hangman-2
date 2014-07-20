using System;
using System.Collections.Generic;
using System.Linq;
using Hangman.IO;

namespace Hangman
{
    /// <summary>
    /// A class responsible for storing player statistics and saving them into file
    /// </summary>
    public class PlayerScore : IComparable
    {
        private string playerName;
        private int numberOfMiskates;
        private int points;
        private IStorageProvider<string, string> storage;

        public PlayerScore(string playerName, IStorageProvider<string, string> storage)
        {
            this.PlayerName = playerName;
            this.storage = storage;
        }

        public string PlayerName
        {
            get { return this.playerName; }
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

        private int? recordPoints;

        public int RecordPoints
        {
            get
            {
                if (!recordPoints.HasValue)
                {
                    LoadRecordData();
                }

                return recordPoints.Value; 
            }
        }

        private int? recordMistakes;

        public int RecordMistakes
        {
            get 
            {
                if (!recordMistakes.HasValue)
                {
                    LoadRecordData();
                }

                return recordMistakes.Value; 
            }
        }

        /// <summary>
        /// Loads the record for this player from a file
        /// </summary>
        public void LoadRecordData()
        {
            var data = storage.LoadEntry(PlayerName).Split(',');
            this.recordPoints = int.Parse(data[0]);
            this.recordMistakes = int.Parse(data[1]);
        }

        /// <summary>
        /// Saves the record to the file
        /// </summary>
        public void SaveRecordData()
        {
            string data = string.Join("," , RecordPoints, RecordMistakes);

            if (storage.ContainsKey(PlayerName))
            {
                storage.UpdateEntry(PlayerName, data);
            }
            else
            {
                storage.AddEntry(PlayerName, data);
            }
        }        


        /// <summary>
        /// Updates the current statistics and the record if needed
        /// </summary>
        public void UpdateCurrentStats(int points, int mistakesCount)
        {
            this.points = points;
            this.numberOfMiskates = mistakesCount;

            if (points > recordPoints)
            {
                recordPoints = points;
            }
            if (mistakesCount < recordMistakes)
            {
                recordMistakes = mistakesCount;
            }
        }

        public int NumberOfMistakes
        {
            get { return this.numberOfMiskates; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of mistakes cannot be negative number!");
                }

                this.numberOfMiskates = value;
            }
        }

        public int Points
        {
            get { return this.points; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                this.points = value;
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
    }
}
