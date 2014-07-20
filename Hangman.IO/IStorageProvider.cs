namespace Hangman.IO
{
    using System;
    using System.Collections.Generic;

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
