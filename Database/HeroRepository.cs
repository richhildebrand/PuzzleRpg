using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class HeroRepository : Repository
    {
        private readonly string HEROES_KEY = "Heroes";

        public HeroRepository()
        {
            CreateKeyIfMissing(HEROES_KEY);
        }

        protected override void CreateKey(string key)
        {
            var heroes = new List<Hero>();
            IsolatedStorageSettings.ApplicationSettings.Add(HEROES_KEY, heroes);
            GivePlayerStartingHeroes();
        }

        public List<Hero> GetHeroesOwnedByPlayer()
        {
            return IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] as List<Hero>;
        }

        public void AddHeroToPlayerCollection(Hero heroToAdd)
        {
            var heroes = GetHeroesOwnedByPlayer();
            heroes.Add(heroToAdd);
            IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] = heroes;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public void RemoveHeroFromPlayerCollection(Hero heroToRemove)
        {
            var heroes = GetHeroesOwnedByPlayer();
            heroes = heroes.Where(h => h.Id != heroToRemove.Id).ToList();
            IsolatedStorageSettings.ApplicationSettings[HEROES_KEY] = heroes;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void GivePlayerStartingHeroes() {
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
