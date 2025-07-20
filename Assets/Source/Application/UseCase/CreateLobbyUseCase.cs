using System.Threading;
using Chess.Domain.Repository;
using Cysharp.Threading.Tasks;

namespace Chess.Application.UseCase
{
    public class CreateLobbyUseCase
    {
        private readonly ILobbyRepository _lobbyRepository;

        public CreateLobbyUseCase(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public async UniTask<bool> ExecuteAsync(string lobbyName, CancellationToken cancellationToken)
        {
            return await _lobbyRepository.CreateLobbyAsync(lobbyName, cancellationToken);
        }
    }
}