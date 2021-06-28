using Fantasy.API.Models;
using MongoDB.Driver;

namespace Fantasy.API.Data
{
    public interface ILeagueContext
    {
        IMongoCollection<FantasyLeague> Leagues { get; }
    }
}
