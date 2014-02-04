using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;


namespace PuzzleRpg.Database
{
    public class TeamRepository : Repository
    {
        private readonly string TEAMS_KEY = "Teams";

        public TeamRepository()
        {
            CreateKeyIfMissing(TEAMS_KEY);
        }

        public List<TeamMemberToSaveToDatabase> GetTeam()
        {
            // TODO: validate team members are in hero database
            return IsolatedStorageSettings.ApplicationSettings[TEAMS_KEY] as List<TeamMemberToSaveToDatabase>;
        }

        public void SaveTeam(List<TeamMember> teamMembers)
        {
            var membersForDatabase = TeamMemberToTeamMemberToSaveInDatabaseMapper.Map(teamMembers);

            IsolatedStorageSettings.ApplicationSettings[TEAMS_KEY] = membersForDatabase;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        protected override void CreateKey(string key)
        {
            var teamMembers = new List<TeamMemberToSaveToDatabase>();
            IsolatedStorageSettings.ApplicationSettings.Add(TEAMS_KEY, teamMembers);
        }
    }
}
