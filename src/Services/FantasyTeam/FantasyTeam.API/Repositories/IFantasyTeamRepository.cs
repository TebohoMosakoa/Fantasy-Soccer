using FantasyTeam.API.Models;
using System.Threading.Tasks;

namespace FantasyTeam.API.Repositories
{
    public interface IFantasyTeamRepository
    {
        Task<Fantasy_Team> GetFantasyTeam(string userName);
        Task<Fantasy_Team> UpdateFantasyTeam(Fantasy_Team team);
        Task DeleteFantasyTeam(string userName);
    }
}
