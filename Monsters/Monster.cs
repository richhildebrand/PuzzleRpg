﻿using System;
using System.Linq;

namespace PuzzleRpg.Monsters
{
    public class Monster
    {
        public string Name { get; set; }
        public int HitPoints { get; private set; }
        public int AttackDamage { get; private set; }

        public string FullImagePath { get; set; }

        public Monster(string name, int hitPoints, int attackDamage)
        {
            Name = name;
            HitPoints = hitPoints;
            AttackDamage = attackDamage;

            FullImagePath = AppGlobals.HeroImagePathPrefix + name + "/" + name + "Full.png";
        }
    }
}
