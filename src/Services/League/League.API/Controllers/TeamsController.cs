using AutoMapper;
using League.API.Dtos.Teams;
using League.API.Models;
using League.API.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace League.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<TeamsController> _logger;
        private readonly IMapper _mapper;

        public TeamsController(ITeamRepository repo, ILogger<TeamsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Team>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams(bool filterValue)
        {
            var teams = await _repo.GetAll(filterValue);

            return Ok(teams);
        }

        [HttpGet("{id}", Name = "GetTeam")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Team), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Team>> GetTeamById(int id)
        {
            var team = await _repo.Get(id);

            if (team == null)
            {
                _logger.LogError($"Team with id: {id}, not found.");
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Team), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] CreateUpdateTeamDto teamDto)
        {
            Team team = _mapper.Map<Team>(teamDto);
            await _repo.Create(team);

            return CreatedAtRoute("GetTeam", new { id = team.Id }, team);
        }

        [HttpPut("{teamId}")]
        [ProducesResponseType(typeof(Team), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateTeam(int teamId, [FromBody] CreateUpdateTeamDto teamDto)
        {
            var team = _repo.Get(teamId);
            Team teamToChange = _mapper.Map(teamDto, team.Result);
            return Ok(await _repo.Update(teamToChange));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Team), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteTeamById(int id)
        {
            return Ok(await _repo.Delete(id));
        }
    }
}
