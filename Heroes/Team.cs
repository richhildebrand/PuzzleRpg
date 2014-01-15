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
            AddHero(0, new Hero("ArrowCat", AppGlobals.Types.Fire));
            AddHero(1, new Hero("BearCat", AppGlobals.Types.Water));
            AddHero(2, new Hero("MoonCat", AppGlobals.Types.Wood));
            AddHero(3, new Hero("SamuriCat", AppGlobals.Types.Wood));
            AddHero(4, new Hero("StackCat", AppGlobals.Types.Fire));
            //AddHero(5, new Hero("UltraCat", AppGlobals.Types.Water));

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
