using AutoMapper;
using Bet.Application.Features.Bets.Commands.Checkout;
using Bet.Application.Features.Bets.Queries.GetBetsList;
using Bet.Domain.Entities;

namespace Bet.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BetEntity, BetsVm>().ReverseMap();
            CreateMap<BetEntity, CheckoutBetCommand>().ReverseMap();
        }
    }
}
