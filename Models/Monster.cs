using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Monster
    {
        public string Name { get; set; }
        public int TotalHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int AttackDamage { get; private set; }
        public int ExpGivenOnDeath { get; private set; }

        public string FullImagePath { get; set; }

        private Monster() { } // needed so it can be saved in local storage

        public Monster(string name, int hitPoints, int attackDamage, int expGivenOnDeath)
        {
            Name = name;
            TotalHealth = hitPoints;
            CurrentHealth = hitPoints;
            AttackDamage = attackDamage;
            ExpGivenOnDeath = expGivenOnDeath;

            FullImagePath = AppGlobals.HeroImagePathPrefix + name + "/" + name + "Full.png";
        }

        public void TakeDamage(int damageTaken)
        {
            CurrentHealth = CurrentHealth - damageTaken;
            CurrentHealth = (CurrentHealth > TotalHealth) ? TotalHealth : CurrentHealth;
        }
    }
}
