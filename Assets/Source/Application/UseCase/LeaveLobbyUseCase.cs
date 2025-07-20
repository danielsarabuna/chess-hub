using System.Threading;
using Chess.Domain.Repository;
using Cysharp.Threading.Tasks;

namespace Chess.Application.UseCase
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