using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League.GRPC.Dtos.Players
{
    public class CreateUpdatePlayerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int JerseyNumber { get; set; }
        public string Position { get; set; }
        public double Price { get; set; }
        public string PlayerImage { get; set; }
        public bool IsDeleted { get; set; }
        public string TeamId { get; set; }
    }
}
