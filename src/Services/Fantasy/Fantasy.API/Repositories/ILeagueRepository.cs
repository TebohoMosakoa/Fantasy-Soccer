using Fantasy.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Repositories
{
    public interface ILeagueRepository
    {
        Task<IEnumerable<FantasyLeague>> GetLeagues();
        Task<FantasyLeague> GetLeague(string id);
        Task<IEnumerable<FantasyLeague>> GetLeagueByName(string name);
        Task CreateLeague(FantasyLeague league);
        Task<bool> UpdateLeague(FantasyLeague league);
        Task<bool> DeleteLeague(string id);
    }
}
