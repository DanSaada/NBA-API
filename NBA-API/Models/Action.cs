    namespace NBA_API.Models
{
      public class Action
    {
        public int ActionNumber { get; set; }
        //public TimeSpan Clock { get; set; }
        //public DateTime TimeActual { get; set; }
        public int Period { get; set; }
        public string PeriodType { get; set; }
        public string ActionType { get; set; }
        public string SubType { get; set; }
        public int PersonId { get; set; }
        public string TeamTricode { get; set; }
        public string Description { get; set; }
        public string PlayerName {get; set;}
        public string PlayerNameI {get; set;}
    }
}
