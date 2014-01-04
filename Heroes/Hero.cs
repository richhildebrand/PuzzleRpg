using System;
using System.Linq;

namespace PuzzleRpg.Heroes
{
    public class Hero
    {
        public string FullImagePath { get; set; }
        public string ProfileImagePath { get; set; }

        public Hero(string heroName)
        {
            FullImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Full.png";
            ProfileImagePath = AppGlobals.HeroImagePathPrefix + heroName + "/" + heroName + "Profile.png";
        }
    }
}
