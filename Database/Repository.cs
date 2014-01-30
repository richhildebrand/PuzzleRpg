using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace PuzzleRpg.Database
{
    public abstract class Repository
    {
        protected abstract void CreateKey(string key);

        protected void CreateKeyIfMissing(string key)
        {
            if (KeyIsMissing(key))
            {
                CreateKey(key);
            }
        }

        private bool KeyIsMissing(string key)
        {
            return !IsolatedStorageSettings.ApplicationSettings.Contains(key);
        }

    }
}
