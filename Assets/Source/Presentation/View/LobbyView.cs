using System.Collections;
using System.Linq;
using System.Threading;
using ChessHub.Application.ViewModel;
using ChessHub.Domain.Model;

namespace ChessHub.Presentation.View
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using VContainer;

    [RequireComponent(typeof(UIDocument))]
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        private ScrollView _playerList;
        private Button _confirmButton;
        private Button _startGameButton;
        private Button _exitButton;

        private VisualElement _root;
        private ILobbyViewModel _viewModel;

        private void Reset()
        {
            uiDocument = GetComponent<UIDocument>();
        }

        [Inject]
        private void Construct(ILobbyViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            _root = uiDocument.rootVisualElement;
            _root.style.flexGrow = 1;

            _playerList = _root.Q<ScrollView>("playerList");
            _confirmButton = _root.Q<Button>("confirmButton");
            _startGameButton = _root.Q<Button>("startGameButton");
            _exitButton = _root.Q<Button>("exitButton");

            _confirmButton.clicked += OnConfirmClicked;
            _startGameButton.clicked += OnStartGameClicked;
            _exitButton.clicked += OnExitGameClicked;

            StartCoroutine(nameof(UpdatePlayerListCoroutine));
        }

        private void OnDestroy()
        {
            if (_confirmButton != null) _confirmButton.clicked -= OnConfirmClicked;
            if (_startGameButton != null) _startGameButton.clicked -= OnStartGameClicked;
            if (_exitButton != null) _exitButton.clicked -= OnExitGameClicked;
        }

        private IEnumerator UpdatePlayerListCoroutine()
        {
            var waitForSeconds = new WaitForSeconds(1);
            while (true)
            {
                UpdatePlayerList();
                yield return waitForSeconds;
            }
        }

        private void UpdatePlayerList()
        {
            _playerList.Clear();

            foreach (var playerLabel in _viewModel.Players.Select(CreatePlayerList))
            {
                _playerList.Add(playerLabel);
            }
        }

        private VisualElement CreatePlayerList(PlayerModel player)
        {
            var playerLabel = new Label(player.Nickname)
            {
                style =
                {
                    marginBottom = 5,
                    fontSize = 20,
                    color = new Color(1, 1, 1)
                }
            };
            return playerLabel;
            ;
        }

        private void OnConfirmClicked()
        {
            Debug.Log("Confirm button clicked! Player ready confirmation.");
            _viewModel.PlayerReadyAsync(CancellationToken.None);
        }

        private void OnStartGameClicked()
        {
            Debug.Log("Start Game button clicked!");
            _viewModel.StartGameAsync(CancellationToken.None);
        }

        private void OnExitGameClicked()
        {
            Debug.Log("Exit Game button clicked!");
            _viewModel.ExitLobbyAsync(CancellationToken.None);
        }
    }
}