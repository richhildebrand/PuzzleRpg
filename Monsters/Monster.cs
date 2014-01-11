using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleRpg.Monsters
{
    public class Monster
    {
        public string FullImagePath { get; set; }

        public Monster(string name)
        {
            FullImagePath = AppGlobals.HeroImagePathPrefix + name + "/" + name + "Full.png";
        }
    }
}
