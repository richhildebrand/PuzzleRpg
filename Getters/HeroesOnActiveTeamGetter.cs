using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Getters
{
    public class HeroesOnActiveTeamGetter
    {
        private TeamRepository _teamRepository;

        public HeroesOnActiveTeamGetter()
        {
            this._teamRepository = new TeamRepository();
        }

        public List<Hero> Get()
        {
            var teamFromDatabase = _teamRepository.GetTeam();
            var teamMembers = TeamMemberMapper.Map(teamFromDatabase.TeamMembers);
            return teamMembers.Select(h => h.ThisHero).ToList();
        }
    }
}
