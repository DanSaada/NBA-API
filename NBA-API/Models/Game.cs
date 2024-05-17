    namespace NBA_API.Models
{
      public class Game
    {
        public int GameId { get; set; }
        public List<Team> Teams { get; set; }
        public List<Action> Actions { get; set; }
    }
}
