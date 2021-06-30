using Bet.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bet.Application.Contracts.Persistence
{
    public interface IBetRepository : IAsyncRepository<BetEntity>
    {
        Task<IEnumerable<BetEntity>> GetBetsByUserName(string userName);
    }
}
