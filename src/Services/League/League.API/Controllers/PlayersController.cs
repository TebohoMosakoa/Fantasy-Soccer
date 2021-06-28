using AutoMapper;
using League.API.Dtos.Players;
using League.API.Models;
using League.API.Repositories.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace League.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _repo;
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerRepository repo, ILogger<PlayersController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Player>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers(bool filterValue)
        {
            var players = await _repo.GetAll(filterValue);

            return Ok(players);
        }

        //[HttpGet("GetPlayersByName/{name}")]
        //[ProducesResponseType(typeof(IEnumerable<Player>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByName(string name)
        //{
        //    var players = await _repo.GetPlayerByName(name);

        //    return Ok(players);
        //}

        //[HttpGet("GetPlayersByTeam/{teamName}")]
        //[ProducesResponseType(typeof(IEnumerable<Player>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByTeam(string teamName)
        //{
        //    var players = await _repo.GetPlayerByTeam(teamName);

        //    return Ok(players);
        //}


        [HttpGet("{id}", Name = "GetPlayer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Player>> GetPlayerById(int id)
        {
            var team = await _repo.Get(id);

            if (team == null)
            {
                _logger.LogError($"Player with id: {id}, not found.");
                return NotFound();
            }

            return Ok(team);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Player>> CreatePlayer([FromBody] CreateUpdatePlayerDto playerDto)
        {
            Player player = _mapper.Map<Player>(playerDto);
            await _repo.Create(player);

            return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] CreateUpdatePlayerDto playerDto)
        {
            var player = _repo.Get(id);
            Player playerToChange = _mapper.Map(playerDto, player.Result);
            return Ok(await _repo.Update(playerToChange));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Player), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePlayerById(int id)
        {
            return Ok(await _repo.Delete(id));
        }
    }
}
