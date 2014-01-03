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
            AddHero(0, new Hero("Assets/Monsters/ArrowCat/ArrowCatProfile.png"));
            AddHero(1, new Hero("Assets/Monsters/BearCat/BearCatProfile.png"));
            AddHero(2, new Hero("Assets/Monsters/MoonCat/MoonCatProfile.png"));
            AddHero(3, new Hero("Assets/Monsters/SamuriCat/SamuriCatProfile.png"));
            AddHero(4, new Hero("Assets/Monsters/StackCat/StackCatProfile.png"));
            AddHero(5, new Hero("Assets/Monsters/UltraCat/UltraCatProfile.png"));
        }

        public void AddHero(int slot, Hero hero) 
        {
            Heroes[slot] = hero;
        }
    }
}
