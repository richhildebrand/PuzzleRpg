using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Heroes
{
    public class Team
    {
        public Hero[] Heroes { get; set; }

        public Team()
        {
            Heroes = new Hero[AppGlobals.MaxHeroesOnATeam];
            AddHero(0, new Hero("ArrowCat"));
            AddHero(1, new Hero("BearCat"));
            AddHero(2, new Hero("MoonCat"));
            AddHero(3, new Hero("SamuriCat"));
            AddHero(4, new Hero("StackCat"));
            AddHero(5, new Hero("UltraCat"));
        }

        public void AddHero(int slot, Hero hero) 
        {
            Heroes[slot] = hero;
        }
    }
}
