using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public static class HeroRepository
    {
        private static readonly string HEROES_KEY = "Heroes";

        public static List<Hero> GetHeroesOwnedByPlayer()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(HEROES_KEY))
            {
                GivePlayerStartingHeroes();
            }
            
            return IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] as List<Hero>;
        }

        public static void AddHeroToPlayerCollection(Hero newHero)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(HEROES_KEY))
            {
                var heroes = new List<Hero>();
                heroes.Add(newHero);
                IsolatedStorageSettings.ApplicationSettings.Add(HEROES_KEY, heroes);
            }
            else
            {
                var heroes = GetHeroesOwnedByPlayer();
                heroes.Add(newHero);
                IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] = heroes;
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private static void GivePlayerStartingHeroes() {
            AddHeroToPlayerCollection(HeroDatabase.GetHero(0));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(1));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(2));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(3));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(4));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(5));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(6));
            AddHeroToPlayerCollection(HeroDatabase.GetHero(7));
        }
    }
}
