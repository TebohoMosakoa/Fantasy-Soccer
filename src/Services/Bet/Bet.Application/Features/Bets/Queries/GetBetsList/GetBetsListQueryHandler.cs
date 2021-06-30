using AutoMapper;
using Bet.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bet.Application.Features.Bets.Queries.GetBetsList
{
    public class GetBetsListQueryHandler : IRequestHandler<GetBetsListQuery, List<BetsVm>>
    {
        private readonly IBetRepository _betRepository;
        private readonly IMapper _mapper;

        public GetBetsListQueryHandler(IBetRepository betRepository, IMapper mapper)
        {
            _betRepository = betRepository ?? throw new ArgumentNullException(nameof(betRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<BetsVm>> Handle(GetBetsListQuery request, CancellationToken cancellationToken)
        {
            var betList = await _betRepository.GetBetsByUserName(request.UserName);
            return _mapper.Map<List<BetsVm>>(betList);
        }
    }
}
