using System;

namespace Hangman.IO
{
    public interface IOManager
    {
        void Print(string message);
        string ReadInput();
    }
}
