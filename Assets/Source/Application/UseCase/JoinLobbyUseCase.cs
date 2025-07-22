using System.Threading;
using ChessHub.Domain.Repository;
using Cysharp.Threading.Tasks;

namespace ChessHub.Application.UseCase
{
    public class JoinLobbyUseCase
    {
        private readonly ILobbyRepository _lobbyRepository;

        public JoinLobbyUseCase(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public async UniTask<bool> ExecuteAsync(string lobbyId, CancellationToken cancellationToken)
        {
            return await _lobbyRepository.JoinLobbyAsync(lobbyId, cancellationToken);
        }
    }
}