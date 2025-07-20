using System;
using Chess.Application.ViewModel;

namespace Presentation.ViewModel
{
    public class LoadingViewModel : ILoadingViewModel
    {
        public event Action<float> OnProgressChanged;

        public float Progress { get; private set; }

        public void SetProgress(float progress)
        {
            Progress = progress;
            OnProgressChanged?.Invoke(progress);
        }
    }
}