using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace League.GRPC.Models
{
    public class Team
    {
        public Team()
        {
            IsDeleted = false;
        }
        [Key]
        public int Id { get; init; }
        public string Name { get; set; }

        public string LogoImage { get; set; }
        public string JerseyImage { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
