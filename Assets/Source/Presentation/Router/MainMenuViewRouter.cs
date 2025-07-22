using System.Threading;
using ChessHub.Application.Router;
using ChessHub.Application.ViewModel;
using ChessHub.Presentation.View;
using Cysharp.Threading.Tasks;

namespace ChessHub.Presentation.Router
{
    public class MainMenuViewRouter : BaseViewRouter<MainMenuView, IMainMenuViewModel>, IMainMenuViewRouter
    {
        public MainMenuViewRouter(MainMenuView view, IMainMenuViewModel viewModel) : base(view, viewModel)
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