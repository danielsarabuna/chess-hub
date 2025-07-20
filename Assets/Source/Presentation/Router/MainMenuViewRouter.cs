using System.Threading;
using Chess.Application.Router;
using Chess.Application.ViewModel;
using Cysharp.Threading.Tasks;
using Presentation.View;

namespace Presentation.Router
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