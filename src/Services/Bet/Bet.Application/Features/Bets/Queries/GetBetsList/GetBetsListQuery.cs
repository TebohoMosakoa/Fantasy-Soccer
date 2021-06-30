using MediatR;
using System;
using System.Collections.Generic;

namespace Bet.Application.Features.Bets.Queries.GetBetsList
{
    public class GetBetsListQuery : IRequest<List<BetsVm>>
    {
        public string UserName { get; set; }
        public GetBetsListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
