using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Heroes;

namespace PuzzleRpg.Database
{
    public static class HeroRepository
    {
        private static readonly string HEROES_KEY = "Heroes";

        public static List<Hero> GetPlayerHeroes()
        {
            var playerHeroes = Repository.Load<List<Hero>>(HEROES_KEY);
            if (playerHeroes == null)
            {
                Repository.Save<List<Hero>>(HEROES_KEY, new List<Hero>());
            }
            return playerHeroes;
        }

        public static void AddPlayerHero(Hero newHero)
        {
            var allHeroes = GetPlayerHeroes();
            allHeroes.Add(newHero);

            Repository.Save<List<Hero>>(HEROES_KEY, allHeroes);
        }
    }
}
