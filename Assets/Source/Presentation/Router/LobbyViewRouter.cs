using System.Threading;
using Chess.Application.Router;
using Chess.Application.ViewModel;
using Cysharp.Threading.Tasks;
using Presentation.View;

namespace Presentation.Router
{
    public class LobbyViewRouter : BaseViewRouter<LobbyView, ILobbyViewModel>, ILobbyViewRouter
    {
        public LobbyViewRouter(LobbyView view, ILobbyViewModel viewModel) : base(view, viewModel)
        {
        }

        public override UniTask Show(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override UniTask Hide(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}