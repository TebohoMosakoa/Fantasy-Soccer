using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League.API.Dtos.Teams
{
    public class CreateUpdateTeamDto
    {
        public string Name { get; set; }
        public string LogoImage { get; set; }
        public string JerseyImage { get; set; }
    }
}
