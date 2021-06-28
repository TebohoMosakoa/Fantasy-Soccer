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

        public FantasyTeamsController(IFantasyTeamRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
