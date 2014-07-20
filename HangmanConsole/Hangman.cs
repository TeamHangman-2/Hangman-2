//The Command pattern
//Template Method pattern
//Observer pattern?? UpdateWordOnScreen
//Mediator pattern
//Memento-Results??
//Singleton

namespace Hangman
{
    using HangmanConsole;
    using System;
    using System.Linq;

    public class Hangman
    {
        public static void Main(string[] args)
        {
            //var player = new PlayerScore("Pasho", 30);
            var ioManager = new ConsoleIOManager();
            var recordManager = new FileRecordManager();


            GameEngine gameEngine = new GameEngine(ioManager);
            gameEngine.Start();
        }
    }
}
