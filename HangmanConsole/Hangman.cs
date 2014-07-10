//The Command pattern
//Template Method pattern
//Observer pattern?? UpdateWordOnScreen
//Mediator pattern
//Memento-Results??
//Singleton

namespace Hangman
{
    using System;
    using System.Linq;

    public class Hangman
    {
        public static void Main(string[] args)
        {
            Player player = new Player("Pasho", 30);
            GameEngine gameEngine = new GameEngine(player);
            gameEngine.Start();
        }
    }
}
