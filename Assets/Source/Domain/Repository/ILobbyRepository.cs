using Chess.Domain.Model;

namespace Chess.Domain.Repository
{
    using System.Collections.Generic;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public interface ILobbyRepository
    {
        IReadOnlyList<PlayerModel> GetPlayerList { get; }
        UniTask<bool> CreateLobbyAsync(string lobbyName, CancellationToken cancellationToken);
        UniTask<bool> JoinLobbyAsync(string lobbyId, CancellationToken cancellationToken);
        UniTask<bool> LeaveLobbyAsync(CancellationToken cancellationToken);
    }
}