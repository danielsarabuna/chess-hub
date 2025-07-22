using System.Threading;
using Cysharp.Threading.Tasks;

namespace ChessHub.Application.UseCase
{
    public class PlayerReadyUseCase
    {
        public UniTask<bool> ExecuteAsync(CancellationToken cancellationToken)
        {
            return UniTask.FromResult(true);
        }
    }
}