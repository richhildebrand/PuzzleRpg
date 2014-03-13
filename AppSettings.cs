using System;
using System.Linq;

namespace PuzzleRpg
{
    public static class  AppSettings
    {
        public static TimeSpan GainStaminaIntervalLength = TimeSpan.FromMinutes(30);
        public static int AmountOfStaminaToAddInterval = 3;

        public static int MaxNumberOfHeroesPlayerCanOwn = 6;

        public static int MonsterDeathFadeTimeInMilliseconds = 800;
        public static int MonsterDeathTimeInvisibleInMilliseconds = 200;
    }
}
