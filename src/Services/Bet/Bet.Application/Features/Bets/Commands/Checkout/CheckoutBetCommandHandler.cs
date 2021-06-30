using AutoMapper;
using Bet.Application.Contracts.Infrastructure;
using Bet.Application.Contracts.Persistence;
using Bet.Application.Models;
using Bet.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bet.Application.Features.Bets.Commands.Checkout
{
    public class CheckoutBetCommandHandler : IRequestHandler<CheckoutBetCommand, int>
    {
        private readonly IBetRepository _betRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutBetCommandHandler> _logger;

        public CheckoutBetCommandHandler(IBetRepository betRepository, IEmailService emailService, IMapper mapper, ILogger<CheckoutBetCommandHandler> logger)
        {
            _betRepository = betRepository ?? throw new ArgumentNullException(nameof(betRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutBetCommand request, CancellationToken cancellationToken)
        {
            var bet = _mapper.Map<BetEntity>(request);
            var newBet = await _betRepository.AddAsync(bet);

            _logger.LogInformation($"Bet {newBet.Id} is successfully created.");

            await SendMail(newBet);

            return newBet.Id;
        }

        private async Task SendMail(BetEntity bet)
        {
            var email = new Email() { To = "mosakoateboho@gmail.com", Body = $"Bet was created.", Subject = "Bet was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bet {bet.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
