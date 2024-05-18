using System.Collections.Generic;
using Xunit;
using NBA_API.Models;
using NBA_API.Services;
using ActionModel = NBA_API.Models.Action;

namespace NBA_API.Tests
{
    public class NBAServiceTests
    {
        private readonly NBAService _nbaService;
        private readonly GamesData _gamesData;

        public NBAServiceTests()
        {
            _gamesData = new GamesData
            {
                Game = new Game
                {
                    GameId = 0022000180,
                    Actions = new List<ActionModel>
                    {
                        new ActionModel { PlayerNameI = "Player 1", ActionType = "3pt", ShotResult = "Made", TeamTricode = "BOS" },
                        new ActionModel { PlayerNameI = "Player 1", ActionType = "assist", AssistPersonId = 1, TeamTricode = "BOS" },
                        new ActionModel { PlayerNameI = "Player 1", ActionType = "rebound", TeamTricode = "BOS" },
                        new ActionModel { PlayerNameI = "Player 2", ActionType = "2pt", ShotResult = "Made", TeamTricode = "ORL" },
                        new ActionModel { PlayerNameI = "Player 2", ActionType = "rebound", TeamTricode = "ORL" }
                    }
                }
            };
            _nbaService = new NBAService(_gamesData);
        }

        [Fact]
        public void GetAllPlayersNames_ShouldReturnPlayerNamesGroupedByTeam()
        {
            // Act
            var result = _nbaService.GetAllPlayersNames();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ContainsKey("BOS"));
            Assert.True(result.ContainsKey("ORL"));
            Assert.Contains("Player 1", result["BOS"]);
            Assert.Contains("Player 2", result["ORL"]);
        }

        [Fact]
        public void GetAllActionTypesByPlayerName_ShouldReturnAllActionTypesForGivenPlayer()
        {
            // Act
            var result = _nbaService.GetAllActionTypesByPlayerName("Player 1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains("3pt", result);
            Assert.Contains("assist", result);
            Assert.Contains("rebound", result);
        }

        [Fact]
        public void GetTopPerformers_ShouldReturnTopPerformers()
        {
            // Act
            var result = _nbaService.GetTopPerformers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var player1 = result.Find(p => p.PlayerName == "Player 1");
            var player2 = result.Find(p => p.PlayerName == "Player 2");

            Assert.NotNull(player1);
            Assert.Equal(3, player1.Points);
            Assert.Equal(1, player1.Assists);
            Assert.Equal(1, player1.Rebounds);

            Assert.NotNull(player2);
            Assert.Equal(2, player2.Points);
            Assert.Equal(0, player2.Assists);
            Assert.Equal(1, player2.Rebounds);
        }
    }
}
