using Bet.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bet.Infrastructure.Persistence
{
    public class BetContextSeed
    {
        public static async Task SeedAsync(BetContext betContext, ILogger<BetContextSeed> logger)
        {
            if (!betContext.Bets.Any())
            {
                betContext.Bets.AddRange(GetPreconfiguredBets());
                await betContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(BetContext).Name);
            }
        }

        private static IEnumerable<BetEntity> GetPreconfiguredBets()
        {
            return new List<BetEntity>
            {
                new BetEntity() {UserName = "Teboho1996", FirstName = "Teboho", LastName = "Mosakoa", EmailAddress = "teboh@gmail.com", AddressLine = "123 CSharp road", Surburb = "Microsoft",City = "Johannesburg", Province = "Gauteng", ZipCode = "1111", JoiningFee = 50 }
            };
        }
    }
}
