using System.Collections.Generic;
using System.Threading;
using Chess.Application.Service;
using Chess.Application.UseCase;
using Chess.Application.ViewModel;
using Chess.Domain.Model;
using Chess.Domain.Repository;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Presentation.ViewModel
{
    public class LobbyViewModel : ILobbyViewModel
    {
        private readonly LeaveLobbyUseCase _leaveLobbyUseCase;
        private readonly CreateGameUseCase _createGameUseCase;
        private readonly PlayerReadyUseCase _playerReadyUseCase;
        private readonly JoinLobbyUseCase _joinLobbyUseCase;
        private readonly ISceneManagement _sceneManagement;
        private readonly ILobbyRepository _lobbyRepository;

        public string LobbyKey { get; set; }
        public string NewLobbyName { get; set; }
        public IReadOnlyList<PlayerModel> Players => _lobbyRepository.GetPlayerList;

        public LobbyViewModel(LeaveLobbyUseCase createLobbyUseCase, CreateGameUseCase createGameUseCase,
            PlayerReadyUseCase playerReadyUseCase, ILobbyRepository lobbyRepository, ISceneManagement sceneManagement)
        {
            _leaveLobbyUseCase = createLobbyUseCase;
            _createGameUseCase = createGameUseCase;
            _playerReadyUseCase = playerReadyUseCase;
            _lobbyRepository = lobbyRepository;
            _sceneManagement = sceneManagement;
        }

        public async UniTask StartGameAsync(CancellationToken cancellationToken)
        {
            var success = await _createGameUseCase.ExecuteAsync(NewLobbyName, cancellationToken);
            if (success)
            {
                await _sceneManagement.LoadSceneAsync("Game", LoadSceneMode.Single);
            }
        }

        public async UniTask ExitLobbyAsync(CancellationToken cancellationToken)
        {
            var success = await _leaveLobbyUseCase.ExecuteAsync(cancellationToken);
            if (success)
            {
                await _sceneManagement.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            }
            else
            {
                await _sceneManagement.LoadSceneAsync("Error", LoadSceneMode.Single);
            }
        }

        public async UniTask PlayerReadyAsync(CancellationToken cancellationToken)
        {
            var success = await _playerReadyUseCase.ExecuteAsync(cancellationToken);
        }
    }
}