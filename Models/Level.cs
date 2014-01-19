using System;
using System.Linq;
using PuzzleRpg.Monsters;

namespace PuzzleRpg.Models
{
    public class Level
    {
        private Monster _stageOne;

        public Level()
        {
            //Fill Monsters?
        }

        public Monster GetStage(int stageNumber)
        {
            return _stageOne;
        }
    }
}