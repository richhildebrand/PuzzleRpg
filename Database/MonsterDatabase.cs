using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Monsters;

namespace PuzzleRpg.Database
{
    public static class MonsterDatabase
    {
        private static List<Monster> _allMonsters;

        static MonsterDatabase()
        {
            _allMonsters = new List<Monster>();
                                         //Name   HP   Attack
            //I made the BlueDragon have a more powerful attack since TotalHealth is now a large number.
            //It was hard to actually see if the HealthBar was working or not
            _allMonsters.Add(new Monster("BlueDragon", 1000, 1000));
            _allMonsters.Add(new Monster("GreenDragon", 1000, 90));
            _allMonsters.Add(new Monster("RedDragon", 1000, 90));
            _allMonsters.Add(new Monster("BigBlue", 300, 20));
            _allMonsters.Add(new Monster("Turtle", 5000, 1));
            _allMonsters.Add(new Monster("Rex", 1000, 10));
            _allMonsters.Add(new Monster("StoneOrc", 500, 30));
            _allMonsters.Add(new Monster("WoodOrc", 500, 30));
        }

        public static int MonsterCount()
        {
            return _allMonsters.Count();
        }

        public static Monster GetMonster(int monsterId)
        {
            var monsterFromDatabase = _allMonsters[monsterId];
            return CopyMonster(monsterFromDatabase);
        }

        public static Monster GetMonster(string monsterName)
        {
            var monsterFromDatabase = _allMonsters.Single(m => m.Name == monsterName);
            return CopyMonster(monsterFromDatabase);
        }

        private static Monster CopyMonster(Monster monsterToCopy)
        {
            return new Monster(monsterToCopy.Name,
                               monsterToCopy.HitPoints,
                               monsterToCopy.AttackDamage);
        }
    }
}
