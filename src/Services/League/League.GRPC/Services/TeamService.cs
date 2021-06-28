using AutoMapper;
using Grpc.Core;
using League.GRPC.Models;
using League.GRPC.Protos;
using League.GRPC.Repositories.Teams;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace League.GRPC.Services
{
    public class TeamService : TeamProtoService.TeamProtoServiceBase
    {
        private readonly ITeamRepository _repository;
        private readonly ILogger<TeamService> _logger;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository repository, ILogger<TeamService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async override Task<TeamModel> CreateTeam(CreateTeamRequest request, ServerCallContext context)
        {
            var team = _mapper.Map<Team>(request.Team);

            await _repository.Create(team);
            _logger.LogInformation("Team is successfully created. Team Name : {Name}", team.Name);

            var teamModel = _mapper.Map<TeamModel>(team);
            return teamModel;
        }

        public async override Task<TeamModel> GetTeam(GetTeamRequest request, ServerCallContext context)
        {
            var team = await _repository.Get(request.Id);
            if (team == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Team with Id={request.Id} is not found."));
            }
            _logger.LogInformation("Team with Id: {Id} retrieved : {Name}", team.Id, team.Name);

            var teamModel = _mapper.Map<TeamModel>(team);
            return teamModel;
        }

        public async override Task<TeamModels> GetTeams(GetTeamsFilerRequest request, ServerCallContext context)
        {
            var teams = await _repository.GetAll(request.IsDeleted);
            if (teams == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No Teams found!"));
            }
            _logger.LogInformation("Teams with the IsDeleted status of {isDeleted} retrieved", request.IsDeleted);
            var teamModels = _mapper.Map<TeamModels>(teams);
            return teamModels;
        }

        public async override Task<TeamModel> GetTeamByName(GetTeamByNameRequest request, ServerCallContext context)
        {
            var teams = await _repository.GetByName(request.Name);
            if (teams == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No Teams with the name={request.Name} found!"));
            }
            _logger.LogInformation("Teams with the Name {Name} retrieved", request.Name);
            var teamModel = _mapper.Map<TeamModel>(teams);
            return teamModel;
        }

        public async override Task<TeamModel> UpdateTeam(UpdateTeamRequest request, ServerCallContext context)
        {
            var team = _mapper.Map<Team>(request.Team);

            await _repository.Update(team);
            _logger.LogInformation("Team is successfully updated. Name : {TeamName} {FirstName}", team.Name);

            var teamModel = _mapper.Map<TeamModel>(team);
            return teamModel;
        }

        public async override Task<DeleteTeamResponse> DeleteTeam(DeleteTeamRequest request, ServerCallContext context)
        {
            var deleted = await _repository.Delete(request.Id);
            var response = new DeleteTeamResponse
            {
                Success = deleted
            };

            return response;
        }
    }
}
