using ChessHub.Domain.Model;

namespace ChessHub.Domain.Repository
{
    using System.Collections.Generic;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public interface ILobbyRepository
    {
        IReadOnlyList<PlayerModel> GetPlayerList { get; }
        UniTask<bool> CreateLobbyAsync(string lobbyName, CancellationToken cancellationToken);
        UniTask<bool> JoinLobbyAsync(string lobbyId, CancellationToken cancellationToken);
        UniTask<bool> LeaveRoomAsync(CancellationToken cancellationToken);
    }
}