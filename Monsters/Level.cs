using System;
using System.Linq;

namespace PuzzleRpg.Monsters
{
    public class Level
    {
        private Monster _stageOne;

        public Level()
        {
            _stageOne = new Monster("hydra");
        }

        public Monster GetStage(int stageNumber)
        {
            return _stageOne;
        }
    }
}