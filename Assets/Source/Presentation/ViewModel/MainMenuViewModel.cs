using System.Collections.Generic;
using ChessHub.Application.Service;
using ChessHub.Application.UseCase;
using ChessHub.Application.ViewModel;
using ChessHub.Domain.Model;
using UnityEngine.SceneManagement;

namespace ChessHub.Presentation.ViewModel
{
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public class MainMenuViewModel : IMainMenuViewModel
    {
        private readonly CreateLobbyUseCase _createLobbyUseCase;
        private readonly JoinLobbyUseCase _joinLobbyUseCase;
        private readonly IUnityMultiplayerService _unityMultiplayerService;
        private readonly ISceneManagement _sceneManagement;

        public string PlayerName
        {
            get => _unityMultiplayerService.PlayerName;
        }

        public IReadOnlyList<LobbyModel> Lobbies => _unityMultiplayerService.GetAvailableLobbies;

        public string LobbyKey { get; set; }
        public string NewLobbyName { get; set; }

        public MainMenuViewModel(CreateLobbyUseCase createLobbyUseCase, JoinLobbyUseCase joinLobbyUseCase,
            IUnityMultiplayerService unityMultiplayerService, ISceneManagement sceneManagement)
        {
            _createLobbyUseCase = createLobbyUseCase;
            _joinLobbyUseCase = joinLobbyUseCase;
            _unityMultiplayerService = unityMultiplayerService;
            _sceneManagement = sceneManagement;
        }

        public async UniTask CreateLobbyAsync(CancellationToken cancellationToken)
        {
            var success = await _createLobbyUseCase.ExecuteAsync(NewLobbyName, cancellationToken);
            if (success)
            {
                await _sceneManagement.LoadSceneAsync("Lobby", LoadSceneMode.Single);
            }
        }

        public async UniTask JoinLobbyAsync(CancellationToken cancellationToken)
        {
            var success = await _joinLobbyUseCase.ExecuteAsync(LobbyKey, cancellationToken);
            if (success)
            {
                await _sceneManagement.LoadSceneAsync("Lobby", LoadSceneMode.Single);
            }
            else
            {
                await _sceneManagement.LoadSceneAsync("Error", LoadSceneMode.Single);
            }
        }
    }
}