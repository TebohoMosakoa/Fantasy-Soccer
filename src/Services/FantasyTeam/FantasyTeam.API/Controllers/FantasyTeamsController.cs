using FantasyTeam.API.GrpcServices;
using FantasyTeam.API.Models;
using FantasyTeam.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FantasyTeam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FantasyTeamsController : ControllerBase
    {
        private readonly IFantasyTeamRepository _repository;
        private readonly PlayerGrpcService _playerGrpcService;

        public FantasyTeamsController(PlayerGrpcService playerGrpcService, IFantasyTeamRepository repository)
        {
            _playerGrpcService = playerGrpcService;
            _repository = repository;
        }

        [HttpGet("{userName}", Name = "GetFantasyTeam")]
        [ProducesResponseType(typeof(Fantasy_Team), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Fantasy_Team>> GetFantasyTeam(string userName)
        {
            var team = await _repository.GetFantasyTeam(userName);
            return Ok(team ?? new Fantasy_Team(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Fantasy_Team), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Fantasy_Team>> UpdateFantasyTeam([FromBody] Fantasy_Team team)
        {
            //This is were we consume the League.GRPC service to get TeamPlayer information from the Player service.
            foreach(var item in team.Players)
            {
                var player = await _playerGrpcService.GetPlayer(item.PlayerId);
                TeamPlayer newPlayer = new TeamPlayer
                {
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Price = player.Price,
                    TeamId = player.TeamId,
                    PlayerId = player.Id,
                    PlayerImage = player.PlayerImage
                };
                team.Players.Add(newPlayer);
            }
            
            return Ok(await _repository.UpdateFantasyTeam(team));
        }

        [HttpDelete("{userName}", Name = "DeleteFantasyTeam")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteFantasyTeam(string userName)
        {
            await _repository.DeleteFantasyTeam(userName);
            return Ok();
        }
    }
}
