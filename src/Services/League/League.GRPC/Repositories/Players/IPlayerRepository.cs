using League.GRPC.Models;
using League.GRPC.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace League.GRPC.Repositories.Players
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<IEnumerable<Player>> GetByTeamName(int teamId);
    }
}
