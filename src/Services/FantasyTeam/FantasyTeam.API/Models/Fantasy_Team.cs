using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyTeam.API.Models
{
    public class Fantasy_Team
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<TeamPlayer> Players { get; set; } = new List<TeamPlayer>();

        public Fantasy_Team()
        {
        }

        public Fantasy_Team(string userName)
        {
            UserName = userName;
        }

        public double TotalPrice
        {
            get
            {
                double totalprice = 0;
                foreach (var item in Players)
                {
                    totalprice += item.Price;
                }
                return totalprice;
            }
        }
    }
}
