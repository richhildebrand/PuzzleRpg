using System;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class TeamRepository : Repository
    {
        private readonly string TEAM_KEY = "Team";

        public TeamRepository()
        {
            CreateKeyIfMissing(TEAM_KEY);
        }

        public Hero[] GetTeam() {
            return IsolatedStorageSettings.ApplicationSettings[TEAM_KEY] as Hero[];
        }

        public void SaveTeam(Hero[] heroes) {
            IsolatedStorageSettings.ApplicationSettings[TEAM_KEY] = heroes;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        protected override void CreateKey(string key)
        {
            var heroes = new Hero[5];
            IsolatedStorageSettings.ApplicationSettings.Add(TEAM_KEY, heroes);
        }
    }
}
