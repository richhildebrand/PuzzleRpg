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

            var heroOne = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));
            var heroTwo = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));
            var heroThree = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));
            var heroFour = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));
            var heroFive = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));

            MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount());
            AddHero(0, heroOne, heroOne.HitPoints);
            AddHero(1, heroTwo, heroTwo.HitPoints);
            AddHero(2, heroThree, heroThree.HitPoints);
            AddHero(3, heroFour, heroFour.HitPoints);
            AddHero(4, heroFive, heroFive.HitPoints);

            CurrentHealth = 100;
            TotalHealth = GetTotalHealth();
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

        public void AddHero(int slot, Hero hero, int hitPoints) 
        {
            Heroes[slot] = hero;
            hitPoints = hero.HitPoints;
        }

        public int GetTotalHealth()
        {
            var hitPoints = 0;
            foreach (var hero in Heroes)
            {
                hitPoints += hero.HitPoints;
            }
            return hitPoints;
        }
    }
}
