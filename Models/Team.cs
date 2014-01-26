using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Models
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

            CurrentHealth = GetTotalHealth();
            TotalHealth = GetTotalHealth();
        }

        public int CalculateDamage(List<OrbMatch> matches)
        {
            var totalDamage = 0;

            if (matches == null) {
                return totalDamage;
            }

            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                var hero = Heroes[i];
                if (hero != null)
                {
                    //TODO: care about sets matching greater than 3 orbs at once
                    var typeMatches = matches.Count(m => m.Type == hero.Type);
                    double heroDamage = hero.AttackDamage * typeMatches;
                    var extraMatches = matches.Count - typeMatches;
                    heroDamage *= (0.25 * extraMatches) + 1;
                    totalDamage += Convert.ToInt32(heroDamage);
                }
            }
            return totalDamage;
        }

        public int GetPercentageOfRemainingHealth()
        {
            var percentage = (double)CurrentHealth / (double)TotalHealth * 100;
            return Convert.ToInt32(percentage);
        }

        public double GetTotalPercentageOfHealPoints()
        {
            var totalHealPoints = 0;
            foreach (var hero in Heroes)
            {
                totalHealPoints += hero.HealsFor;
            }
            var percentageToReturn = ((double)totalHealPoints / (double)TotalHealth) * 100;
            return percentageToReturn;
        }

        public void TakeDamage(int monsterAttackDamage)
        {
            CurrentHealth -= monsterAttackDamage;
        }

        public void Heal(int amountToHeal)
        {
            CurrentHealth += amountToHeal;
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

        public double GetPercentageOfRemainingHealth(int currentHealth)
        {
            var percentageToHeal = GetTotalPercentageOfHealPoints();
            var currentPercentage = currentHealth / (double)TotalHealth * 100;
            var totalPercent = currentPercentage + percentageToHeal;
            if (totalPercent > 100)
            {
                totalPercent = 100;
            }
            CurrentHealth = Convert.ToInt32(((double)totalPercent / 100) * TotalHealth);
            return totalPercent;
        }
    }
}
