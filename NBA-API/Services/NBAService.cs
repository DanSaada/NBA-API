using System;
using System.Collections.Generic;
using System.Linq;
using NBA_API.Models;

namespace NBA_API.Services
{
    public class NBAService
    {
        private readonly GamesData _gamesData;

        public NBAService(GamesData gamesData)
        {
            _gamesData = gamesData;
        }

        public Dictionary<string, List<string>> GetAllPlayersNames()
        {
            var result = new Dictionary<string, List<string>>();

            foreach (var game in _gamesData.Games)
            {
                foreach (var team in game.Teams)
                {
                    var teamName = team.TeamTricode;
                    if (!result.ContainsKey(teamName))
                    {
                        result[teamName] = new List<string>();
                    }

                    foreach (var player in team.Players)
                    {
                        result[teamName].Add(player.PlayerName);
                    }
                }
            }

            return result;
        }
    }
}
