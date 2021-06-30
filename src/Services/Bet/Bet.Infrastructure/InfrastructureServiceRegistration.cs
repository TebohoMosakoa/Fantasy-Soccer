using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bet.Application.Contracts.Infrastructure;
using Bet.Application.Contracts.Persistence;
using Bet.Application.Models;
using Bet.Infrastructure.Persistence;
using Bet.Infrastructure.Repositories;

namespace Bet.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BetContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BettingConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IBetRepository, BetRepository>();

            //services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            //services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
   
}
