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
        private readonly HeroRepository _heroRepository;
        private const int FIRST_TEAM_ID = 1;

        public TeamRepository()
        {
            _heroRepository = new HeroRepository();
            CreateKeyIfMissing(TEAMS_KEY);
        }

        public TeamToSaveToDatabase GetTeam(int teamId = FIRST_TEAM_ID)
        {
            var teams = GetTeams();
            var teamToGet = teams.Single(t => t.TeamId == teamId);
            teamToGet.TeamMembers = RemoveHeroesNoLongerOwnedByPlayer(teamToGet.TeamMembers);

            return teamToGet;
        }

        public void SaveTeam(Team team)
        {
            var teamMemberForDatabase = TeamMemberMapper.Map(team.TeamMembers);
            var teamForDatabase = new TeamToSaveToDatabase(team.TeamId, teamMemberForDatabase);
            SaveTeam(teamForDatabase);
        }

        public void SaveTeam(TeamToSaveToDatabase team)
        {
            List<TeamToSaveToDatabase> teams = GetTeams();
            var unmodifiedTeams = teams.Where(t => t.TeamId != team.TeamId).ToList();
            unmodifiedTeams.Add(team);

            IsolatedStorageSettings.ApplicationSettings[TEAMS_KEY] = unmodifiedTeams;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private List<TeamToSaveToDatabase> GetTeams() 
        {
            return IsolatedStorageSettings.ApplicationSettings[TEAMS_KEY] as List<TeamToSaveToDatabase>;
        }

        private List<TeamMemberToSaveToDatabase> RemoveHeroesNoLongerOwnedByPlayer(List<TeamMemberToSaveToDatabase> teamMembers)
        {
            var ownedHeroes = _heroRepository.GetHeroesOwnedByPlayer();
            var ownedHeroIds = ownedHeroes.Select(h => h.Id);
            return teamMembers.Where(tm => ownedHeroIds.Contains(tm.HeroId)).ToList();
        }

        protected override void CreateKey(string key)
        {
            var teams = new List<TeamToSaveToDatabase>();
            var firstTeam = new TeamToSaveToDatabase(FIRST_TEAM_ID, new List<TeamMember>());
            teams.Add(firstTeam);
            IsolatedStorageSettings.ApplicationSettings.Add(TEAMS_KEY, teams);
        }
    }
}
