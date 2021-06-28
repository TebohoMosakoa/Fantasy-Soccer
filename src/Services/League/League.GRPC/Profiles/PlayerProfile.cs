using AutoMapper;
using League.GRPC.Models;
using League.GRPC.Protos;

namespace League.GRPC.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerModel>().ReverseMap();
        }
    }
}
