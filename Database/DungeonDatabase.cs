using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class DungeonDatabase
    {
        public List<Dungeon> AllDungeons { get; set; }

        public DungeonDatabase()
        {
            AllDungeons = new List<Dungeon>();

            AllDungeons.Add(GetRexsHouse(id: 1, stam: 1, isAvailable: true));
            AllDungeons.Add(GetOrcVilla(id: 2, stam: 1));
            AllDungeons.Add(GetTurtleShore(id: 3, stam: 1));
            AllDungeons.Add(GetGreenDragonsDen(id: 4, stam: 1));
            AllDungeons.Add(GetBlueDragonsDen(id: 5, stam: 1));
            AllDungeons.Add(GetDragonsDen(id: 6, stam: 3));
        }

        private Dungeon GetTurtleShore(int id, int stam)
        {
            var dungeon = new Dungeon(id, "Turtle Shore", stam);
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Turtle")));
            return dungeon;
        }

        private Dungeon GetRexsHouse(int id, int stam, bool isAvailable)
        {
            var dungeon = new Dungeon(id, "Rex's House", stam);
            dungeon.IsAvailable = isAvailable;
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Rex"))); 
            return dungeon;
        }

        private Dungeon GetDragonsDen(int id, int stam)
        {
            var dungeon = new Dungeon(id, "Dragons Den", stam);
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("GreenDragon")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BlueDragon")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("RedDragon")));
            return dungeon;
        }

        private Dungeon GetOrcVilla(int id, int stam)
        {
            var dungeon = new Dungeon(id, "Orc Villa", stam);
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            return dungeon;
        }

        private Dungeon GetGreenDragonsDen(int id, int stam)
        {
            var dungeon = new Dungeon(id, "Green Dragons Den", stam);
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Turtle")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("GreenDragon")));
            return dungeon;
        }

        private Dungeon GetBlueDragonsDen(int id, int stam)
        {
            var dungeon = new Dungeon(id, "Blue Dragons Den", stam);
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BigBlue")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BlueDragon")));
            return dungeon;
        }
    }
}
