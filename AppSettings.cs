using System;
using System.Linq;

namespace PuzzleRpg
{
    public static class  AppSettings
    {
        public static TimeSpan GainStaminaIntervalLength = TimeSpan.FromMinutes(30);
        public static int AmountOfStaminaToAddInterval = 3;

        public static int MaxNumberOfHeroesPlayerCanOwn = 20;

        public static int MonsterDeathFadeTimeInMilliseconds = 800;
        public static int MonsterDeathTimeInvisibleInMilliseconds = 200;

        public static double ExpericencePerLevelMultiplier = 2;
    }
}
