using League.API.Models;
using League.API.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace League.API.Repositories.Players
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<IEnumerable<Player>> GetByTeamName(int teamId);
    }
}
