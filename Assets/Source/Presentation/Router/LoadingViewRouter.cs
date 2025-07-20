using System;
using System.Collections.Generic;
using System.Threading;
using Chess.Application.Router;
using Chess.Application.ViewModel;
using Cysharp.Threading.Tasks;
using Presentation.View;
using UnityEngine;

namespace Presentation.Router
{
    public class LoadingViewRouter : BaseViewRouter<LoadingView, ILoadingViewModel>, ILoadingViewRouter
    {
        public LoadingViewRouter(LoadingView view, ILoadingViewModel viewModel) : base(view, viewModel)
        {
        }

        public override UniTask Show(CancellationToken cancellationToken)
        {
            View.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide(CancellationToken cancellationToken)
        {
            View.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public async UniTask Execute(IList<Func<UniTask>> taskList, CancellationToken token)
        {
            ViewModel.SetProgress(0F);

            await Show(token);
            var startTime = Time.time;

            for (var i = 0; i < taskList.Count; i++)
            {
                await taskList[i].Invoke();
                ViewModel.SetProgress(Convert.ToSingle(i) / taskList.Count);
            }

            ViewModel.SetProgress(1);

            const float minimumDuration = 1F;
            var duration = Time.time - startTime;
            var excessTime = minimumDuration - duration;
            if (excessTime > 0F) await UniTask.Delay(TimeSpan.FromSeconds(excessTime), cancellationToken: token);

            await Hide(token);
        }
    }
}