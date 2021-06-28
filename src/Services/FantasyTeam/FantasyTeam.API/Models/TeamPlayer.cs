namespace FantasyTeam.API.Models
{
    public class TeamPlayer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Price { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string PlayerImage { get; set; }
    }
}
