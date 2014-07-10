using System;

namespace Hangman.IO
{
    public interface IOManager
    {
        void Print(string message);

        void Print(string message, params object[] args);

        string ReadInput();
    }
}
