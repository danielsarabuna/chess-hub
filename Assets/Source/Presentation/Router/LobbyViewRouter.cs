using System.Threading;
using ChessHub.Application.Router;
using ChessHub.Application.ViewModel;
using ChessHub.Presentation.View;
using Cysharp.Threading.Tasks;

namespace ChessHub.Presentation.Router
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