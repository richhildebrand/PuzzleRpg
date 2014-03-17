using System;
using System.Linq;
using PuzzleRpg.Logic;

namespace PuzzleRpg.Models
{
    public class Hero
    {
        private readonly HeroLevelCalculator _levelCalculator;
        private readonly int _baseExpPerLevel;

        private readonly int _healingPerLevel;
        public int HealsFor { get { return _healingPerLevel * Level; } }

        private readonly int _hitPointsPerLevel;
        public int HitPoints { get { return _hitPointsPerLevel * Level; } }

        private readonly int _attackDamagePerLevel;
        public int AttackDamage { get { return _attackDamagePerLevel * Level; } }

        public double CurrentExp { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AppGlobals.Types Type { get; private set; }

        public string FullImagePath { get; set; }
        public string ProfileImagePath { get; set; }

        private Hero() {} // Really C#?

        public Hero(string heroName, 
                    int hitPointsPerLevel, 
                    int attackDamagePerLevel, 
                    AppGlobals.Types type,
                    int healingPerLevel)
        {
            CurrentExp = 0;
            Id = Guid.NewGuid();
            Name = heroName;
            Type = type;

            _baseExpPerLevel = 250;
            _levelCalculator = new HeroLevelCalculator();
            _hitPointsPerLevel = hitPointsPerLevel;
            _attackDamagePerLevel = attackDamagePerLevel;
            _healingPerLevel = healingPerLevel;

            FullImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Full.png";
            ProfileImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Profile.png";
        }

        public int Level
        {
            get { return _levelCalculator.GetLevelFrom(_baseExpPerLevel, CurrentExp); }
        }
    }
}
