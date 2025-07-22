using System.Threading;
using ChessHub.Domain.Repository;
using Cysharp.Threading.Tasks;

namespace ChessHub.Application.UseCase
{
    public class LeaveLobbyUseCase
    {
        private readonly ILobbyRepository _lobbyRepository;

        public LeaveLobbyUseCase(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await _lobbyRepository.LeaveLobbyAsync(cancellationToken);
        }
    }
}