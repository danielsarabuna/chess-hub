using System.Collections.Generic;
using System.Threading;
using ChessHub.Domain.Model;
using Cysharp.Threading.Tasks;

namespace ChessHub.Application.ViewModel
{
    public interface ILobbyViewModel
    {
        public IReadOnlyList<PlayerModel> Players { get; }

        UniTask StartGameAsync(CancellationToken cancellationToken);
        UniTask ExitLobbyAsync(CancellationToken cancellationToken);
        UniTask PlayerReadyAsync(CancellationToken cancellationToken);
    }
}