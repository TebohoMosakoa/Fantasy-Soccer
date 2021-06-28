using Fantasy.API.Models;
using Fantasy.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Fantasy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueRepository _repository;
        private readonly ILogger<LeaguesController> _logger;

        public LeaguesController(ILeagueRepository repository, ILogger<LeaguesController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FantasyLeague>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<FantasyLeague>>> GetLeagues()
        {
            var leagues = await _repository.GetLeagues();
            return Ok(leagues);
        }

        [HttpGet("{id:length(24)}", Name = "GetLeague")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(FantasyLeague), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FantasyLeague>> GetLeagueById(string id)
        {
            var league = await _repository.GetLeague(id);

            if (league == null)
            {
                _logger.LogError($"League with id: {id}, not found.");
                return NotFound();
            }

            return Ok(league);
        }

        //[Route("[action]/{category}", Name = "GetProductByCategory")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<League>>> GetProductByCategory(string category)
        //{
        //    var products = await _repository.GetProductByCategory(category);
        //    return Ok(products);
        //}

        [Route("[action]/{name}", Name = "GetLeagueByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<FantasyLeague>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<FantasyLeague>>> GetLeagueByName(string name)
        {
            var items = await _repository.GetLeagueByName(name);
            if (items == null)
            {
                _logger.LogError($"Leagues with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FantasyLeague), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FantasyLeague>> CreateLeague([FromBody] FantasyLeague league)
        {
            await _repository.CreateLeague(league);

            return CreatedAtRoute("GetLeague", new { id = league.Id }, league);
        }

        [HttpPut]
        [ProducesResponseType(typeof(FantasyLeague), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateLeague([FromBody] FantasyLeague league)
        {
            return Ok(await _repository.UpdateLeague(league));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteLeague")]
        [ProducesResponseType(typeof(FantasyLeague), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteLeagueById(string id)
        {
            return Ok(await _repository.DeleteLeague(id));
        }
    }
}
