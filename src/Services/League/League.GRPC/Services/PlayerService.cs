using AutoMapper;
using Grpc.Core;
using League.GRPC.Models;
using League.GRPC.Protos;
using League.GRPC.Repositories.Players;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace League.GRPC.Services
{
    public class PlayerService : PlayerProtoService.PlayerProtoServiceBase
    {
        private readonly IPlayerRepository _repository;
        private readonly ILogger<PlayerService> _logger;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository repository, ILogger<PlayerService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async override Task<PlayerModel> GetPlayer(GetPlayerRequest request, ServerCallContext context)
        {
            var player = await _repository.Get(request.Id);
            if (player == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Player with Id={request.Id} is not found."));
            }

            _logger.LogInformation("Player is retrieved with the name : {lastname} {firstname}", player.LastName, player.FirstName);

            var playerModel = _mapper.Map<PlayerModel>(player);
            return playerModel;
        }

        public async override Task<PlayerModels> GetPlayers(GetPlayersFilerRequest request, ServerCallContext context)
        {
            var players = await _repository.GetAll(request.IsDeleted);
            if (players == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No PLayers found!"));
            }
            _logger.LogInformation("Players with the IsDeleted status of {isDeleted} retrieved", request.IsDeleted);
            var playerModels = _mapper.Map<PlayerModels>(players);
            return playerModels;
        }

        public async override Task<PlayerModels> GetPlayersByName(GetPlayerByNameRequest request, ServerCallContext context)
        {
            var players = await _repository.GetByName(request.Name);
            if (players == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No PLayers found!"));
            }
            _logger.LogInformation("Players with the Name {Name} retrieved", request.Name);
            var playerModels = _mapper.Map<PlayerModels>(players);
            return playerModels;
        }

        public async override Task<PlayerModels> GetPlayersByTeam(GetPlayersByTeamRequest request, ServerCallContext context)
        {
            var players = await _repository.GetByTeamName(request.TeamId);
            if (players == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No PLayers found!"));
            }
            _logger.LogInformation("Players with the Team Id: {Id} retrieved", request.TeamId);
            var playerModels = _mapper.Map<PlayerModels>(players);
            return playerModels;
        }

        public async override Task<PlayerModel> CreatePlayer(CreatePlayerRequest request, ServerCallContext context)
        {
            var player = _mapper.Map<Player>(request.Player);

            await _repository.Create(player);
            _logger.LogInformation("Player is successfully created. Name : {LastName} {FirstName}", player.LastName, player.FirstName);

            var playerModel = _mapper.Map<PlayerModel>(player);
            return playerModel;
        }

        public async override Task<PlayerModel> UpdatePlayer(UpdatePlayerRequest request, ServerCallContext context)
        {
            var player = _mapper.Map<Player>(request.Player);

            await _repository.Update(player);
            _logger.LogInformation("Player is successfully updated. Name : {LastName} {FirstName}", player.LastName, player.FirstName);

            var playerModel = _mapper.Map<PlayerModel>(player);
            return playerModel;
        }

        public async override Task<DeletePlayerResponse> DeletePlayer(DeletePlayerRequest request, ServerCallContext context)
        {
            var deleted = await _repository.Delete(request.Id);
            var response = new DeletePlayerResponse
            {
                Success = deleted
            };

            return response;
        }
    }
}
