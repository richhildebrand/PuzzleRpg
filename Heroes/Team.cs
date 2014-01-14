using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Heroes
{
    public class Team
    {
        public Hero[] Heroes { get; set; }
        public int TotalHealth { get; set; }
        public int CurrentHealth { get; set; }

        public Team()
        {
            Heroes = new Hero[AppGlobals.MaxHeroesOnATeam];
            AddHero(0, new Hero("ArrowCat"));
            AddHero(1, new Hero("BearCat"));
            AddHero(2, new Hero("MoonCat"));
            AddHero(3, new Hero("SamuriCat"));
            AddHero(4, new Hero("StackCat"));
            AddHero(5, new Hero("UltraCat"));

            CurrentHealth = 100;
            TotalHealth = 100;
        }

        public int GetPercentageOfRemainingHealth()
        {
            var percentage = (double)CurrentHealth / (double)TotalHealth * 100;
            return Convert.ToInt32(percentage);
        }

        public void TakeDamage(int monsterAttackDamage)
        {
            CurrentHealth -= monsterAttackDamage;
        }

        public void AddHero(int slot, Hero hero) 
        {
            Heroes[slot] = hero;
        }
    }
}
