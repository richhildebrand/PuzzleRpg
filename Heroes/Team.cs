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
            AddHero(0, new Hero("Assets/Monsters/ArrowCat.jpg"));
            AddHero(1, new Hero("Assets/Monsters/BearCat.jpg"));
            AddHero(2, new Hero("Assets/Monsters/MoonCat.jpg"));
            AddHero(3, new Hero("Assets/Monsters/SamuriCat.jpg"));
            AddHero(4, new Hero("Assets/Monsters/StackCat.jpg"));
            AddHero(5, new Hero("Assets/Monsters/UltraCat.jpg"));
        }

        public void AddHero(int slot, Hero hero) 
        {
            Heroes[slot] = hero;
        }
    }
}
