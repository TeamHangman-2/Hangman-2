using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class Player
    {
        private string playerName;
        private int numberOfMiskates;

        public Player(string playerName, int numberOfMistakes)
        {
            this.PlayerName = playerName;
            this.NumberOfMistakes = numberOfMistakes;
        }

        public string PlayerName 
        {
            get { return this.playerName; }
            set
            {
                if (value==null)
                {
                    throw new ArgumentNullException("Player name cannot be null!");
                }

                if (value==string.Empty)
                {
                    throw new ArgumentException("Player name cannot be empty string!");
                }

                this.playerName = value;
            }
        }

        public int NumberOfMistakes 
        {
            get { return this.numberOfMiskates; }
            set
            {
                if (value<0)
                {
                    throw new ArgumentException("Number of mistakes cannot be negative number!");
                }

                this.numberOfMiskates = value;
            } 
        }
        public int Compare(Player otherPlayer)
        {
            if (this.NumberOfMistakes <= otherPlayer.NumberOfMistakes)
                return -1;
            else
                return 1;// the newer one replaces the older
        }
    }
}
