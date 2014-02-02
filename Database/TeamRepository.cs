using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class TeamRepository : Repository
    {
        private readonly string TEAM_KEY = "Team";

        public TeamRepository()
        {
            CreateKeyIfMissing(TEAM_KEY);
        }

        public List<TeamMember> GetTeam() {
            return IsolatedStorageSettings.ApplicationSettings[TEAM_KEY] as List<TeamMember>;
        }

        public void SaveTeam(List<TeamMember> teamMembers)
        {
            IsolatedStorageSettings.ApplicationSettings[TEAM_KEY] = teamMembers;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        protected override void CreateKey(string key)
        {
            var teamMembers = new List<TeamMember>();
            IsolatedStorageSettings.ApplicationSettings.Add(TEAM_KEY, teamMembers);
        }
    }
}
