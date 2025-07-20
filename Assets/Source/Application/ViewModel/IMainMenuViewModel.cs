using System.Collections.Generic;
using System.Threading;
using Chess.Domain.Model;
using Cysharp.Threading.Tasks;

namespace Chess.Application.ViewModel
{
    public interface IMainMenuViewModel
    {
        string LobbyKey { get; set; }
        string NewLobbyName { get; set; }
        string PlayerName { get; set; }
        
        public IReadOnlyList<LobbyModel> Lobbies  { get; }

        UniTask CreateLobbyAsync(CancellationToken cancellationToken);
        UniTask JoinLobbyAsync(CancellationToken cancellationToken);
    }
}