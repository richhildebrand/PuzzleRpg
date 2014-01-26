using System;
using System.Linq;

namespace PuzzleRpg.Monsters
{
    public class Monster
    {
        public string Name { get; set; }
        public int TotalHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int AttackDamage { get; private set; }

        public string FullImagePath { get; set; }

        public Monster(string name, int hitPoints, int attackDamage)
        {
            Name = name;
            TotalHealth = hitPoints;
            CurrentHealth = hitPoints;
            AttackDamage = attackDamage;

            FullImagePath = AppGlobals.HeroImagePathPrefix + name + "/" + name + "Full.png";
        }

        public void TakeDamage(int damageTaken)
        {
            CurrentHealth = CurrentHealth - damageTaken;
            CurrentHealth = (CurrentHealth > TotalHealth) ? TotalHealth : CurrentHealth;
        }

        public double GetTotalPercentageOfHealPoints()
        {
            var percentageToReturn = ((double)CurrentHealth / (double)TotalHealth) * 100;
            return percentageToReturn;
        }
    }
}
