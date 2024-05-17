    namespace NBA_API.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamTricode { get; set; }
        public List<Player> Players { get; set; }
    }
}
