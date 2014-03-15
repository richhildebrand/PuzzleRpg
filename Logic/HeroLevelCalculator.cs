using System;
using System.Linq;

namespace PuzzleRpg.Logic
{
    public class HeroLevelCalculator
    {
        public double GetLevelFrom(double baseExp, double currentExp) {
            var levelComparedToBaseExp = currentExp / baseExp + 1;
            var heroLevel = Math.Log(levelComparedToBaseExp, AppSettings.ExpericencePerLevelMultiplier);
            return Math.Floor(heroLevel) + 1;
        }
    }
}
