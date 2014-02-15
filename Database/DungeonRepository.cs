using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class DungeonRepository : Repository
    {
        private readonly string DUNGEONS_KEY = "Dungeons";

        public DungeonRepository()
        {
            CreateKeyIfMissing(DUNGEONS_KEY);
        }

        protected override void CreateKey(string key)
        {
            var dungeonDatabase = new DungeonDatabase();
            var dungeons = dungeonDatabase.AllDungeons;
            IsolatedStorageSettings.ApplicationSettings.Add(DUNGEONS_KEY, dungeons);
        }

        public void UnlockDungon(int dungeonIdToUnlock)
        {
            var allDungeons = GetAllDungeons();
            var dungeonToUnlock = allDungeons.SingleOrDefault(d => d.Id == dungeonIdToUnlock);

            if (dungeonToUnlock != null)
            {
                dungeonToUnlock.IsAvailable = true;
            }

            IsolatedStorageSettings.ApplicationSettings[DUNGEONS_KEY] = allDungeons;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public List<Dungeon> GetUnlockedDungeons()
        {
            var allDungeons = GetAllDungeons();
            var unlockedDungeons = allDungeons.Where(d => d.IsAvailable);
            return unlockedDungeons.ToList();
        }

        private List<Dungeon> GetAllDungeons()
        {
            return IsolatedStorageSettings.ApplicationSettings[DUNGEONS_KEY] as List<Dungeon>;
        }
    }
}
