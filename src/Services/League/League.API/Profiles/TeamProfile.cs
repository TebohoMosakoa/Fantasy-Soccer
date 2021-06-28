using AutoMapper;
using League.API.Dtos.Teams;
using League.API.Models;

namespace League.API.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<CreateUpdateTeamDto, Team>();
        }
    }
}
