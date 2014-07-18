using Hangman.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    public class PlayerScore : IComparable
    {
        private string playerName;
        private int numberOfMiskates;
        private int points;
        private SortedSet<int> personalRecord;
        private IStorageProvider<string> storage;

        public PlayerScore(string playerName, int numberOfMistakes)
        {
            this.Points = 0;
            this.PlayerName = playerName;
            this.NumberOfMistakes = numberOfMistakes;
            this.personalRecord = new SortedSet<int>();
        }

        public static PlayerScore ReadPlayerData(IOManager manager)
        {
            manager.Print("Please enter your name: ");
            string name = manager.ReadInput();

            try
            {
                var playerScore = new PlayerScore(name, 0);
                return playerScore;
            }
            catch (Exception ex)
            {
                manager.Print("An error occured: {0}", ex.Message);
                return ReadPlayerData(manager);
            }
        }


        /// <summary>
        /// Loads the data from the score file for the current player
        /// </summary>
        public void LoadData()
        {
           
            
        }

        public void SaveData()
        {
            

        }

        public string PlayerName
        {
            get { return this.playerName; }
            set
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

        public SortedSet<int> Record
        {
            get { return this.personalRecord; }
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

        public void SetRecord(SortedSet<int> record)
        {
            this.personalRecord = record;
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
