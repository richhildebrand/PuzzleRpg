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

        public void Heal(List<OrbMatch> matches)
        {
            var healMatches = matches.Count(m => m.Type == AppGlobals.Types.Heal);
            var totalHealing = 0;

            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                var hero = Heroes[i];
                if (hero != null)
                {
                    //TODO: care about sets matching greater than 3 orbs at once
                    double heroHealing = hero.HealsFor * healMatches;
                    var extraMatches = matches.Count - healMatches;
                    heroHealing *= (0.25 * extraMatches) + 1;
                    totalHealing += Convert.ToInt32(heroHealing);
                }
            }

            CurrentHealth += totalHealing;
            CurrentHealth = (CurrentHealth > TotalHealth) ? TotalHealth : CurrentHealth;
        }

        public int CalculateDamage(List<OrbMatch> matches)
        {
            return TeamUtils.CalculateDamage(Heroes, matches);
        }

        public void TakeDamage(int monsterAttackDamage)
        {
            CurrentHealth -= monsterAttackDamage;
        }

        private void AddHero(int slot, Hero hero, int hitPoints) 
        {
            Heroes[slot] = hero;
            hitPoints = hero.HitPoints;
        }

        private int GetTotalHealth()
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
