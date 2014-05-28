using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class PlayerScore
    {
        public string PlayerName { get; set; }
        public int NumberOfMistakes { get; set; }

        public PlayerScore(string playerName, int numberOfMistakes)
        {
            this.PlayerName = playerName;
            this.NumberOfMistakes = numberOfMistakes;
        }

        public int Compare(PlayerScore otherPlayer)
        {
            if (this.NumberOfMistakes <= otherPlayer.NumberOfMistakes)
                return -1;
            else
                return 1;// the newer one replaces the older
        }
    }
}
