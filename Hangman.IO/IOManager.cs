namespace Hangman.IO
{
    using System;

    public interface IOManager
    {
        void Print(string message);

        void Print(string message, params object[] args);

        string ReadInput();

        void ClearOutputWindow();
    }
}