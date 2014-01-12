using System;
using System.Linq;

namespace PuzzleRpg.Monsters
{
    public class Monster
    {
        public string FullImagePath { get; set; }
        public int HitPoints { get; private set; }

        public Monster(string name, int hitPoints)
        {
            HitPoints = hitPoints;
            FullImagePath = AppGlobals.HeroImagePathPrefix + name + "/" + name + "Full.png";
        }
    }
}
