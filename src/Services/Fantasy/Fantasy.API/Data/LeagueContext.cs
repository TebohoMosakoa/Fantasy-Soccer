using Fantasy.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Fantasy.API.Data
{
    public class LeagueContext : ILeagueContext
    {
        public LeagueContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Leagues = database.GetCollection<FantasyLeague>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            LeagueContextSeed.SeedData(Leagues);
        }
        public IMongoCollection<FantasyLeague> Leagues { get; }
    }
}
