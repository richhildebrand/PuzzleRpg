using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public static class HeroDatabase
    {
        private static List<Hero> _allHeroes;

        static HeroDatabase()
        {
            _allHeroes = new List<Hero>();
            _allHeroes.Add(new Hero("BlueDragon", 1000, 90, 200, AppGlobals.Types.Water, 250, 10));
            _allHeroes.Add(new Hero("GreenDragon", 1000, 90, 200, AppGlobals.Types.Wood, 250, 10));
            _allHeroes.Add(new Hero("RedDragon", 1000, 90, 200, AppGlobals.Types.Fire, 250, 10));
            _allHeroes.Add(new Hero("BigBlue", 1000, 90, 200, AppGlobals.Types.Water, 250, 10));
            _allHeroes.Add(new Hero("Turtle", 1000, 90, 200, AppGlobals.Types.Wood, 250, 10));
            _allHeroes.Add(new Hero("Rex", 1000, 90, 200, AppGlobals.Types.Fire, 250, 10));
            _allHeroes.Add(new Hero("StoneOrc", 1000, 90, 200, AppGlobals.Types.Earth, 250, 10));
            _allHeroes.Add(new Hero("WoodOrc", 1000, 90, 200, AppGlobals.Types.Wood, 250, 10));
        }

        public static int HeroCount()
        {
            return _allHeroes.Count();
        }

        public static Hero GetHero(int heroId)
        {
            var heroFromDatabase = _allHeroes[heroId];
            return CopyHero(heroFromDatabase);
        }

        private static Hero CopyHero(Hero heroToCopy)
        {
            return new Hero(heroToCopy.Name,
                            heroToCopy.HitPoints,
                            heroToCopy.HealsFor,
                            heroToCopy.AttackDamage,
                            heroToCopy.Type,
                            heroToCopy.BaseExpPerLevel,
                            heroToCopy.MaxLevel);
        }
    }
}
