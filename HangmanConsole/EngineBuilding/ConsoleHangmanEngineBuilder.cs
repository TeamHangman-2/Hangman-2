﻿namespace HangmanConsole
{
    using Hangman;
    using Hangman.EngineBuilding;
    using Hangman.IO;
    using Hangman.ScoreManagement;
    using Hangman.WordGeneration;

    /// <summary>
    /// Class that define the steps of building game engine
    /// </summary>
    public class ConsoleHangmanEngineBuilder : IHangmanEngineBuilder
    {
        private IStorageProvider<string, string> storageProvider;
        private IOManager ioManager;
        private IWordGenerator wordGenerator;
        private IScoreManager scoreManager;

        public void AddIoManager()
        {
            this.ioManager = new ConsoleIOManager();
        }

        public void AddWordGenerator()
        {
            this.wordGenerator = new RandomWordGenerator();
        }

        public void AddScoreManager()
        {
            this.scoreManager = new ScoreManager(this.storageProvider);
        }

        public void AddStorageProvider(string baseDirectory = "", string fileExtension = ".csv")
        {
            this.storageProvider = new HangmanStorage(baseDirectory, fileExtension);
        }

        public GameEngine BuildGameEngine()
        {
            var gameEngine = new GameEngine(this.ioManager, this.scoreManager, this.wordGenerator);
            return gameEngine;
        }
    }
}
