using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace League.GRPC.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryConnection = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Magrating postgesql database.");

                    using NpgsqlConnection connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    command.CommandText = "DROP TABLE IF EXISTS Teams";
                    command.ExecuteNonQuery();

                    command.CommandText = "DROP TABLE IF EXISTS Players";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Teams(Id SERIAL PRIMARY KEY, 
                                                                Name VARCHAR(24) NOT NULL,
                                                                LogoImage VARCHAR(250) NOT NULL,
                                                                JerseyImage VARCHAR(250) NOT NULL,
                                                                IsDeleted boolean)";
                    command.ExecuteNonQuery();


                    command.CommandText = "INSERT INTO Teams(Name, LogoImage, JerseyImage, IsDeleted) VALUES('Arsenal FC', 'https://fantasy.premierleague.com/dist/img/badges/badge_3_40.png','https://fantasy.premierleague.com/dist/img/shirts/standard/shirt_3-66.webp', false);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Teams(Name, LogoImage, JerseyImage, IsDeleted) VALUES('Manchester City FC', 'https://fantasy.premierleague.com/dist/img/badges/badge_43_40.png', 'https://fantasy.premierleague.com/dist/img/shirts/standard/shirt_43-66.webp', false);";
                    command.ExecuteNonQuery();


                    command.CommandText = @"CREATE TABLE Players(Id SERIAL PRIMARY KEY, 
                                                                FirstName VARCHAR(24) NOT NULL,
                                                                LastName VARCHAR(24) NOT NULL,
                                                                Age INT NOT NULL,
                                                                JerseyNumber INT NOT NULL,
                                                                Position VARCHAR(24) NOT NULL,
                                                                Price DOUBLE PRECISION NOT NULL,
                                                                PlayerImage VARCHAR(250) NOT NULL,
                                                                IsDeleted boolean,
                                                                TeamId TEXT)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database.");
                }
                catch (NpgsqlException ex)
                {

                    logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if (retryConnection < 50)
                    {
                        retryConnection++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryConnection);
                    }
                }
            }
            return host;
        }
    }
}
