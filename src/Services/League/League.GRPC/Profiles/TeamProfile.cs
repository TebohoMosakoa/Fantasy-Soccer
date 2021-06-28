using AutoMapper;
using League.GRPC.Models;
using League.GRPC.Protos;

namespace League.GRPC.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamModel>().ReverseMap();
        }
    }
}
