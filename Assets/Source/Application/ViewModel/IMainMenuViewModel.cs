using System.Collections.Generic;
using System.Threading;
using ChessHub.Domain.Model;
using Cysharp.Threading.Tasks;

namespace ChessHub.Application.ViewModel
{
    public interface IMainMenuViewModel
    {
        string LobbyKey { get; set; }
        string NewLobbyName { get; set; }
        string PlayerName { get; }

        public IReadOnlyList<LobbyModel> Lobbies { get; }

        UniTask CreateLobbyAsync(CancellationToken cancellationToken);
        UniTask JoinLobbyAsync(CancellationToken cancellationToken);
    }
}