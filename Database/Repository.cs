using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

namespace PuzzleRpg.Database
{
    public class Repository
    {
        public static void Save<T>(string bucket, T key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(bucket))
            {
                IsolatedStorageSettings.ApplicationSettings[bucket] = key;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(bucket, key);
            }

            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static T Load<T>(string bucket)
        {
            //if (!IsolatedStorageSettings.ApplicationSettings.Contains(bucket))
            //{
            //    IsolatedStorageSettings.ApplicationSettings.Add(bucket, new T());
            //}

            T valueFromBucket;
            IsolatedStorageSettings.ApplicationSettings.TryGetValue<T>(bucket, out valueFromBucket);
            return valueFromBucket;
        }
    }
}
