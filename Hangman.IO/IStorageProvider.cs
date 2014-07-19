using System;
using System.Collections.Generic;

namespace Hangman.IO
{
    public interface IStorageProvider<TKey, TData>
    {
        TData LoadEntry(TKey key);

        void UpdateEntry(TKey key, TData newValue);

        void AddEntry(TKey key, TData newValue);

        bool ContainsKey(TKey key);

        void RemoveEntry(TKey key);

        IEnumerable<TData> GetTop(int count);
    }
}
