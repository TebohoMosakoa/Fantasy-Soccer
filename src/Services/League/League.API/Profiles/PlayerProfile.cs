using AutoMapper;
using League.API.Dtos.Players;
using League.API.Models;

namespace League.API.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<CreateUpdatePlayerDto, Player>();
        }
    }
}
