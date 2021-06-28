using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace League.API.Models
{
    public class Player
    {
        public Player()
        {
            IsDeleted = false;
        }
        [Key]
        public int Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int JerseyNumber { get; set; }
        public string Position { get; set; }
        public double Price { get; set; }
        public string PlayerImage { get; set; }
        public bool IsDeleted { get; set; }
        public string TeamId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        
    }
}
