using System.Threading;
using Chess.Application.Router;
using Cysharp.Threading.Tasks;

namespace Presentation.Router
{
    public abstract class BaseViewRouter<TView, TViewModel> : IViewRouter
    {
        protected readonly TView View;
        protected readonly TViewModel ViewModel;

        protected BaseViewRouter(TView view, TViewModel viewModel)
        {
            View = view;
            ViewModel = viewModel;
        }

        public abstract UniTask Show(CancellationToken cancellationToken);
        public abstract UniTask Hide(CancellationToken cancellationToken);
    }
}