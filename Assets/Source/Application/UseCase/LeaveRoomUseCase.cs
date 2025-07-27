using System.Threading;
using ChessHub.Domain.Repository;
using Cysharp.Threading.Tasks;

namespace ChessHub.Application.UseCase
{
    public class LeaveRoomUseCase
    {
        private readonly ILobbyRepository _lobbyRepository;

        public LeaveRoomUseCase(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await _lobbyRepository.LeaveRoomAsync(cancellationToken);
        }
    }
}