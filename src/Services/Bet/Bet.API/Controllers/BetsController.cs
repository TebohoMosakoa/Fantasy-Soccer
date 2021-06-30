using Bet.Application.Features.Bets.Commands.Checkout;
using Bet.Application.Features.Bets.Queries.GetBetsList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BetsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetBet")]
        [ProducesResponseType(typeof(IEnumerable<BetsVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BetsVm>>> GetBetsByUserName(string userName)
        {
            var query = new GetBetsListQuery(userName);
            var bets = await _mediator.Send(query);
            return Ok(bets);
        }

        // testing purpose
        [HttpPost(Name = "CheckoutBet")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutBet([FromBody] CheckoutBetCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
