using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Heroes;
using PuzzleRpg.Monsters;

namespace PuzzleRpg.Database
{
    public static class HeroDatabase
    {
        private static List<Hero> _allHeroes;

        static HeroDatabase()
        {
            _allHeroes = new List<Hero>();
                                    //Name  HP  Attack  Type
            _allHeroes.Add(new Hero("BlueDragon", 1000, 90, AppGlobals.Types.Water));
            _allHeroes.Add(new Hero("GreenDragon", 1000, 90, AppGlobals.Types.Wood));
            _allHeroes.Add(new Hero("RedDragon", 1000, 90, AppGlobals.Types.Fire));
            _allHeroes.Add(new Hero("BigBlue", 300, 20, AppGlobals.Types.Water));
            _allHeroes.Add(new Hero("Turtle", 5000, 1, AppGlobals.Types.Wood));
            _allHeroes.Add(new Hero("Rex", 1000, 10, AppGlobals.Types.Fire));
            _allHeroes.Add(new Hero("StoneOrc", 500, 30, AppGlobals.Types.Water));
            _allHeroes.Add(new Hero("WoodOrc", 500, 30, AppGlobals.Types.Wood));
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
                            heroToCopy.Type);
        }
    }
}
