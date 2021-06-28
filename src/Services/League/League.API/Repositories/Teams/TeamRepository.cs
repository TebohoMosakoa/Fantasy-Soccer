using Dapper;
using League.API.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League.API.Repositories.Teams
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IConfiguration _configuration;

        public TeamRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> Create(Team entity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            var affected =
            await connection.ExecuteAsync
                ("INSERT INTO Teams(Name, LogoImage, JerseyImage, IsDeleted) VALUES(@Name, @Badge, @Jersey, @IsDeleted);",
                        new { entity.Name, Badge = entity.LogoImage, Jersey = entity.JerseyImage, IsDeleted = false });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            var affected = await connection.ExecuteAsync
                    ("UPDATE Teams SET IsDeleted=@IsDeleted WHERE Id = @Id",
                            new { IsDeleted = true, Id = id });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Team> Get(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            var team = await connection.QueryFirstOrDefaultAsync<Team>("SELECT * FROM Teams WHERE Id = @Id", new { Id = id });

            return team;

        }

        public async Task<IEnumerable<Team>> GetAll(bool isDeleted)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            var teams = await connection.QueryAsync<Team>
               ("SELECT * FROM Teams WHERE IsDeleted = @IsDeleted", new { IsDeleted = isDeleted });
            return teams;
        }

        public async Task<IEnumerable<Team>> GetByName(string name)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            var teams = await connection.QueryAsync<Team>("SELECT * FROM Teams WHERE Name = @Name", new { Name = name });

            return teams;
        }

        public async Task<bool> Update(Team entity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            var affected = await connection.ExecuteAsync
               ("UPDATE Teams SET Name=@Name, LogoImage = @Logo, JerseyImage = @Jersey WHERE Id = @Id",
                       new { entity.Name, Logo = entity.LogoImage, Jersey = entity.JerseyImage, entity.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
