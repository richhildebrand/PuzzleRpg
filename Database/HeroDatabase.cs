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
                                    //Name  HP  Attack  Type HealsFor
            _allHeroes.Add(new Hero("BlueDragon", 1000, 90, AppGlobals.Types.Water, 200));
            _allHeroes.Add(new Hero("GreenDragon", 1000, 90, AppGlobals.Types.Wood, 200));
            _allHeroes.Add(new Hero("RedDragon", 1000, 90, AppGlobals.Types.Fire, 200));
            _allHeroes.Add(new Hero("BigBlue", 300, 20, AppGlobals.Types.Water, 60));
            _allHeroes.Add(new Hero("Turtle", 50, 1, AppGlobals.Types.Wood, 1000));
            _allHeroes.Add(new Hero("Rex", 1000, 10, AppGlobals.Types.Fire, 200));
            _allHeroes.Add(new Hero("StoneOrc", 500, 30, AppGlobals.Types.Water, 100));
            _allHeroes.Add(new Hero("WoodOrc", 500, 30, AppGlobals.Types.Wood, 100));
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
                            heroToCopy.AttackDamage,
                            heroToCopy.Type,
                            heroToCopy.HealsFor);
        }
    }
}
