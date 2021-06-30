using Bet.Application.Contracts.Persistence;
using Bet.Domain.Entities;
using Bet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bet.Infrastructure.Repositories
{
    public class BetRepository : RepositoryBase<BetEntity>, IBetRepository
    {
        public BetRepository(BetContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<BetEntity>> GetBetsByUserName(string userName)
        {
            var orderList = await _dbContext.Bets
                                .Where(o => o.UserName == userName)
                                .ToListAsync();
            return orderList;
        }
    }
}
