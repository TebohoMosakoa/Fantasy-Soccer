using League.GRPC.Protos;
using System;
using System.Threading.Tasks;

namespace FantasyTeam.API.GrpcServices
{
    public class PlayerGrpcService
    {
        private readonly PlayerProtoService.PlayerProtoServiceClient _playerProtoService;

        public PlayerGrpcService(PlayerProtoService.PlayerProtoServiceClient playerProtoService)
        {
            _playerProtoService = playerProtoService ?? throw new ArgumentNullException(nameof(playerProtoService));
        }

        public async Task<PlayerModel> GetPlayer(int id)
        {
            var playerRequest = new GetPlayerRequest { Id = id };
            return await _playerProtoService.GetPlayerAsync(playerRequest);
        }
    }
}
