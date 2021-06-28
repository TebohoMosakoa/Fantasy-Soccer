using Fantasy.API.Data;
using Fantasy.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ILeagueContext _context;

        public LeagueRepository(ILeagueContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateLeague(FantasyLeague league)
        {
            await _context.Leagues.InsertOneAsync(league);
        }

        public async Task<bool> DeleteLeague(string id)
        {
            FilterDefinition<FantasyLeague> filter = Builders<FantasyLeague>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Leagues
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<FantasyLeague> GetLeague(string id)
        {
            return await _context
                           .Leagues
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FantasyLeague>> GetLeagueByName(string name)
        {
            FilterDefinition<FantasyLeague> filter = Builders<FantasyLeague>.Filter.ElemMatch(p => p.Name, name);

            return await _context
                            .Leagues
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<FantasyLeague>> GetLeagues()
        {
            return await _context.Leagues.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateLeague(FantasyLeague league)
        {
            var updateResult = await _context
                                        .Leagues
                                        .ReplaceOneAsync(filter: g => g.Id == league.Id, replacement: league);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
