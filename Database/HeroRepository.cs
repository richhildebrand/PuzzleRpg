using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Heroes;

namespace PuzzleRpg.Database
{
    public static class HeroRepository
    {
        private static readonly string HEROES_KEY = "Heroes";

        public static List<Hero> GetPlayerHeroes()
        {
            var heroes = IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] as List<Hero>;
            return heroes;
        }

        public static void AddPlayerHero(Hero newHero)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(HEROES_KEY))
            {
                var heroes = new List<Hero>();
                heroes.Add(newHero);
                IsolatedStorageSettings.ApplicationSettings.Add(HEROES_KEY, heroes);
            }
            else
            {
                var heroes = GetPlayerHeroes();
                heroes.Add(newHero);
                IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] = heroes;
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
