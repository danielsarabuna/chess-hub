using System.Collections;
using ChessHub.Application.ViewModel;

namespace ChessHub.Presentation.View
{
    using System.Threading;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VContainer;

    [RequireComponent(typeof(UIDocument))]
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        private VisualElement _root;
        private IMainMenuViewModel _viewModel;

        private ScrollView _mainScrollView;
        private TextField _lobbyNameInput;
        private TextField _lobbyKeyInput;
        private Button _createLobbyButton;
        private Button _joinLobbyButton;

        private VisualElement _namePromptContainer;
        private TextField _playerNameInput;
        private Button _confirmNameButton;

        private void Reset()
        {
            uiDocument = GetComponent<UIDocument>();
        }

        [Inject]
        private void Construct(IMainMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Start()
        {
            _root = uiDocument.rootVisualElement;

            var tabsContainer = _root.Q<VisualElement>("tabs-container");

            tabsContainer.style.display = DisplayStyle.None;

            _namePromptContainer = _root.Q<VisualElement>("name-prompt-container");
            _playerNameInput = _namePromptContainer.Q<TextField>("player-name-input");
            _confirmNameButton = _namePromptContainer.Q<Button>("confirm-name-button");

            if (PlayerPrefs.HasKey("PlayerName"))
            {
                _viewModel.PlayerName = PlayerPrefs.GetString("PlayerName");
                ShowTabs();
            }
            else
            {
                ShowNamePrompt();
            }
        }

        private void ShowNamePrompt()
        {
            _namePromptContainer.style.display = DisplayStyle.Flex;

            _confirmNameButton.clicked += () =>
            {
                if (string.IsNullOrWhiteSpace(_playerNameInput.text)) return;
                PlayerPrefs.SetString("PlayerName", _playerNameInput.text);
                _viewModel.PlayerName = _playerNameInput.text;

                ShowTabs();
            };
        }

        private void ShowTabs()
        {
            _namePromptContainer.style.display = DisplayStyle.None;

            var tabsContainer = _root.Q<VisualElement>("tabs-container");
            tabsContainer.style.display = DisplayStyle.Flex;

            SetUpTabs();
        }

        private void SetUpTabs()
        {
            _lobbyNameInput = _root.Q<TextField>("lobby-name-input");
            _lobbyKeyInput = _root.Q<TextField>("lobby-key-input");
            _createLobbyButton = _root.Q<Button>("create-lobby-button");
            _joinLobbyButton = _root.Q<Button>("join-lobby-button");
            _mainScrollView = _root.Q<ScrollView>("ScrollView");
            _root.Q<Label>("playerNameLabel").text = $"Player: {_viewModel.PlayerName}";

            _createLobbyButton.clicked += async () =>
            {
                _viewModel.NewLobbyName = _lobbyNameInput.text;
                await _viewModel.CreateLobbyAsync(CancellationToken.None);
            };

            _joinLobbyButton.clicked += async () =>
            {
                _viewModel.LobbyKey = _lobbyKeyInput.text;
                await _viewModel.JoinLobbyAsync(CancellationToken.None);
            };

            StartCoroutine(nameof(UpdateLobbyListCoroutine));
        }

        private IEnumerator UpdateLobbyListCoroutine()
        {
            var waitForSeconds = new WaitForSeconds(1);
            while (true)
            {
                PopulateLobbyList();
                yield return waitForSeconds;
            }
        }

        private void PopulateLobbyList()
        {
            _mainScrollView.Clear();

            Debug.Log("Lobby List updated");

            foreach (var lobby in _viewModel.Lobbies)
            {
                var lobbyEntry = new Button
                {
                    text = $"{lobby.LobbyName} ({lobby.CurrentPlayers}/{lobby.MaxPlayers})",
                    style =
                    {
                        width = 300,
                        backgroundColor = new StyleColor(new Color(0.9f, 0.9f, 0.9f))
                    }
                };

                lobbyEntry.clicked += () => _viewModel.JoinLobbyAsync(CancellationToken.None);

                _mainScrollView.Add(lobbyEntry);
            }
        }
    }
}