using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NBA_API.Models;
using ActionModel = NBA_API.Models.Action;

namespace NBA_API.Services
{
    public class NBAService
    {
        private readonly GamesData _gamesData;

        public NBAService(GamesData gamesData, ILogger<NBAService> logger)
        {
            _gamesData = gamesData;
        }

        public Dictionary<string, List<string>> GetAllPlayersNames()
        {
            var result = new Dictionary<string, List<string>>();

            if (_gamesData == null || _gamesData.Game == null)
            {
                return result;
            }

            var game = _gamesData.Game;

            foreach (var action in game.Actions ?? new List<ActionModel>())
            {
                var teamName = action.TeamTricode;
                if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(action.PlayerNameI)) continue;

                if (!result.ContainsKey(teamName))
                {
                    result[teamName] = new List<string>();
                }

                if (!result[teamName].Contains(action.PlayerNameI))
                {
                    result[teamName].Add(action.PlayerNameI);
                }
            }

            return result;
        }

        public List<string> GetAllActionTypesByPlayerName(string playerName)
        {
            var result = new List<string>();

            if (_gamesData == null || _gamesData.Game == null)
            {
                return result;
            }

            var game = _gamesData.Game;

            foreach (var action in game.Actions ?? new List<ActionModel>())
            {
                if ((action.PlayerName != null && action.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase)) ||
                    (action.PlayerNameI != null && action.PlayerNameI.Contains(playerName, StringComparison.OrdinalIgnoreCase)))
                {
                    result.Add(action.ActionType);
                }
            }

            return result;
        }

         public List<TopPerformer> GetTopPerformers()
        {
            var result = new Dictionary<string, TopPerformer>();

            if (_gamesData == null || _gamesData.Game == null)
            {
                return result.Values.ToList();
            }

            var game = _gamesData.Game;

            foreach (var action in game.Actions ?? new List<ActionModel>())
            {
                if (string.IsNullOrEmpty(action.PlayerNameI)) continue;

                if (!result.ContainsKey(action.PlayerNameI))
                {
                    result[action.PlayerNameI] = new TopPerformer
                    {
                        PlayerName = action.PlayerNameI,
                        Points = 0,
                        Assists = 0,
                        Rebounds = 0
                    };
                }

                var performer = result[action.PlayerNameI];

                switch (action.ActionType)
                {
                    case "3pt":
                        if (action.ShotResult == "Made") performer.Points += 3;
                        break;
                    case "2pt":
                        if (action.ShotResult == "Made") performer.Points += 2;
                        break;
                    case "freethrow":
                        if (action.ShotResult == "Made") performer.Points += 1;
                        break;
                    case "assist":
                        if (action.AssistPersonId != 0) performer.Assists += 1;
                        break;
                    case "rebound":
                        performer.Rebounds += 1;
                        break;
                }
            }

            return result.Values.OrderByDescending(p => p.Points + p.Assists + p.Rebounds).ToList();
        }

    }
}
