using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Hero
    {
        public int HitPoints { get; private set; }
        public int AttackDamage { get; private set; }
        public string Name { get; set; }
        public AppGlobals.Types Type { get; private set; }
        public int HealsFor { get; private set; }

        public string FullImagePath { get; set; }
        public string ProfileImagePath { get; set; }
        public string SlashProfileImagePath { get; set; }

        private Hero() {} // Really C#?

        public Hero(string heroName, int hitpoints, int attackDamage, AppGlobals.Types type, int healsFor)
        {
            this.Name = heroName;
            this.AttackDamage = attackDamage;
            this.HitPoints = hitpoints;
            Type = type;
            this.HealsFor = healsFor;

            FullImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Full.png";
            ProfileImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Profile.png";
            SlashProfileImagePath = "/" + ProfileImagePath;
        }
    }
}
