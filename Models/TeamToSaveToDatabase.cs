using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Models
{
    public class TeamToSaveToDatabase
    {
        public int TeamId { get; set; }
        public List<TeamMemberToSaveToDatabase> TeamMembers { get; set; }

        private TeamToSaveToDatabase() { } // needed to save in database

        public TeamToSaveToDatabase(int id, List<TeamMemberToSaveToDatabase> teamMembers)
        {
            TeamId = id;
            TeamMembers = teamMembers;
        }

        public TeamToSaveToDatabase(int id, List<TeamMember> teamMembers)
        {
            TeamId = id;

            var membersForDatabase = TeamMemberMapper.Map(teamMembers);
            TeamMembers = membersForDatabase;
        }
    }
}
