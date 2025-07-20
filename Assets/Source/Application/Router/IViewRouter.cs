using System.Threading;
using Cysharp.Threading.Tasks;

namespace Chess.Application.Router
{
    public interface IViewRouter
    {
        UniTask Show(CancellationToken cancellationToken);
        UniTask Hide(CancellationToken cancellationToken);
    }
}