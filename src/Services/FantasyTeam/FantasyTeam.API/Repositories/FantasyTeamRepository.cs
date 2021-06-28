using FantasyTeam.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FantasyTeam.API.Repositories
{
    public class FantasyTeamRepository : IFantasyTeamRepository
    {
        private readonly IDistributedCache _redisCache;

        public FantasyTeamRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteFantasyTeam(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<Fantasy_Team> GetFantasyTeam(string userName)
        {
            var team = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(team))
                return null;

            return JsonConvert.DeserializeObject<Fantasy_Team>(team);
        }


        public async Task<Fantasy_Team> UpdateFantasyTeam(Fantasy_Team team)
        {
            await _redisCache.SetStringAsync(team.UserName, JsonConvert.SerializeObject(team));

            return await GetFantasyTeam(team.UserName);
        }
    }
}
