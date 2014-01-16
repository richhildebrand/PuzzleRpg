using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

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

            MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount());
            AddHero(0, HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount())));
            AddHero(1, HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount())));
            AddHero(2, HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount())));
            AddHero(3, HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount())));
            AddHero(4, HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount())));

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
