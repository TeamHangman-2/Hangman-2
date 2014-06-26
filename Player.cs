﻿using System;
using System.Linq;

namespace Hangman
{
    public class Player:IComparable
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
                if (value < 0)
                {
                    throw new ArgumentException("Number of mistakes cannot be negative number!");
                }

                this.numberOfMiskates = value;
            }
        }

        public int CompareTo(object otherPlayer)
        {
            Player playerToCompareWith = otherPlayer as Player;
            int result = 0;

            if (this.NumberOfMistakes <= playerToCompareWith.NumberOfMistakes)
            {
                result= -1;
            }
            else
            {
                result= 1;
            }

            return result;
        }
    }
}
