using System;

namespace Chess.Application.ViewModel
{
    public interface ILoadingViewModel
    {
        event Action<float> OnProgressChanged; 
        
        float Progress { get; }
        
        void SetProgress(float progress);
    }
}