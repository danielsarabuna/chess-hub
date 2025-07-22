using System;
using ChessHub.Application.ViewModel;

namespace ChessHub.Presentation.ViewModel
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