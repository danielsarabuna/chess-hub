using Chess.Application.ViewModel;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Presentation.View
{
    [RequireComponent(typeof(UIDocument))]
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        private VisualElement _root;
        private ILoadingViewModel _viewModel;
        private VisualElement _progressBar;

        private void Reset()
        {
            uiDocument = GetComponent<UIDocument>();
        }

        [Inject]
        private void Construct(ILoadingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            _root = uiDocument.rootVisualElement;

            _progressBar = _root.Q<VisualElement>("progress-bar");

            _viewModel.OnProgressChanged += UpdateProgressBar;
        }

        private void OnDisable()
        {
            if (_viewModel != null)
            {
                _viewModel.OnProgressChanged -= UpdateProgressBar;
            }
        }

        private void UpdateProgressBar(float progress)
        {
            if (_progressBar != null)
            {
                _progressBar.style.width = new Length(progress * 100, LengthUnit.Percent);
            }
        }
    }
}