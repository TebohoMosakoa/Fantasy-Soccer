using Bet.Domain.Common;
using Bet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bet.Infrastructure.Persistence
{
    public class BetContext : DbContext
    {
        public BetContext(DbContextOptions<BetContext> options) : base(options)
        {

        }

        public DbSet<BetEntity> Bets { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.Now;
                        //to be changed when user service is created and subscribed to
                        entry.Entity.CreatedBy = "Teboho";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LateModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Teboho";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
