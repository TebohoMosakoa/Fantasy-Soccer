using Dapper;
using League.GRPC.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace League.GRPC.Repositories.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IConfiguration _configuration;

        public PlayerRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> Create(Player entity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Players(FirstName, LastName, Age, JerseyNumber, Position, Price, PlayerImage, IsDeleted, TeamId) VALUES(@FirstName, @LastName, @DateOfBirth, @JerseyNumber, @Postion, @Price, @PlayerImage, @IsDeleted, @TeamId);",
                            new { entity.FirstName, entity.LastName, entity.Age, entity.JerseyNumber, entity.Position, entity.Price, entity.PlayerImage, IsDeleted = false, entity.TeamId });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync
                    ("UPDATE Players SET IsDeleted=@IsDeleted WHERE Id = @Id",
                            new { IsDeleted = true, Id = id });


            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Player> Get(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var player = await connection.QueryFirstOrDefaultAsync<Player>
                ("SELECT * FROM Players WHERE Id = @Id", new { Id = id });

            return player;
        }

        public async Task<IEnumerable<Player>> GetAll(bool isDeleted)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var players = await connection.QueryAsync<Player>
                ("SELECT * FROM Players WHERE IsDeleted = @IsDeleted", new { IsDeleted = isDeleted });
            return players;
        }

        public async Task<IEnumerable<Player>> GetByName(string name)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var players = await connection.QueryAsync<Player>
                ("SELECT * FROM Players WHERE FirstName = @Name OR LastName = @Surname", new { Name = name, Surname = name });
            return players;
        }

        public async Task<IEnumerable<Player>> GetByTeamName(int teamId)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var players = await connection.QueryAsync<Player>
                ("SELECT * FROM Players WHERE TeamId = @Id", new { Id = teamId });
            return players;
        }

        public async Task<bool> Update(Player entity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected =
                await connection.ExecuteAsync
                    ("UPDATE Players SET FirstName=@Name, LastName=@Surname, Age=@Age, JerseyNumber=@Jersey, Position=@Position, Price=@Price, PlayerImage=@Image, TeamId=@TeamId  WHERE Id = @Id",
                            new { Name = entity.FirstName, Surname = entity.LastName, entity.Age, Jersey = entity.JerseyNumber, entity.Position, entity.Price, Image = entity.PlayerImage, entity.TeamId, entity.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
