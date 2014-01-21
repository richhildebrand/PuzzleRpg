using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Hero
    {
        public Guid Id { get; set; }
        public int HitPoints { get; private set; }
        public int AttackDamage { get; private set; }
        public string Name { get; set; }
        public AppGlobals.Types Type { get; private set; }
        public int HealsFor { get; private set; }

        public string FullImagePath { get; set; }
        public string ProfileImagePath { get; set; }

        private Hero() {} // Really C#?

        public Hero(string heroName, int hitpoints, int attackDamage, AppGlobals.Types type, int healsFor)
        {
            Id = Guid.NewGuid();
            Name = heroName;
            AttackDamage = attackDamage;
            HitPoints = hitpoints;
            Type = type;
            this.HealsFor = healsFor;

            FullImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Full.png";
            ProfileImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Profile.png";
        }
    }
}
