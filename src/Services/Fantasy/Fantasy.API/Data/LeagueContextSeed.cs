using Fantasy.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Fantasy.API.Data
{
    public class LeagueContextSeed
    {
        public static void SeedData(IMongoCollection<FantasyLeague> collection)
        {
            bool existProduct = collection.Find(p => true).Any();
            if (!existProduct)
            {
                collection.InsertManyAsync(GetPreconfiguredLeagues());
            }
        }

        private static IEnumerable<FantasyLeague> GetPreconfiguredLeagues()
        {
            return new List<FantasyLeague>()
            {
                new FantasyLeague()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Major League",
                    JoiningFee = 50.00M
                },
                new FantasyLeague()
                {
                    Id = "602d2149e773f2a3990b47f1",
                    Name = "Minor League",
                    JoiningFee = 10.00M
                }
            };
        }
    }
}
