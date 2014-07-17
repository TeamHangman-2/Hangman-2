using System;
using System.Collections.Generic;

namespace Hangman.IO
{
    public interface IStorageProvider<T>
    {
        T LoadEntry(string key);

        void UpdateKey(string key, T newValue);

        void RemoveKey(string key);

        IEnumerable<T> GetTop(int count);
    }
}
