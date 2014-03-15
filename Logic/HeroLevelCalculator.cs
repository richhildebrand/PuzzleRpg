using System;
using System.Linq;

namespace PuzzleRpg.Logic
{
    public class HeroLevelCalculator
    {
        public double GetLevelFrom(double baseExpPerLevel, double currentExp)
        {
            var levelComparedToBaseExp = currentExp / baseExpPerLevel + 1;
            var heroLevel = Math.Log(levelComparedToBaseExp, AppSettings.ExpericencePerLevelMultiplier);
            return Math.Floor(heroLevel) + 1;
        }

        public double GetExpNeededForLevel(int level, double baseExpPerLevel)
        {   
            var multiplier = Math.Pow(AppSettings.ExpericencePerLevelMultiplier, level - 1) -1;
            return baseExpPerLevel * multiplier;
        }
    }
}
