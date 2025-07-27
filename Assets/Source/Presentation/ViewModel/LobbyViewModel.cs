using System.Collections.Generic;
using System.Threading;
using ChessHub.Application.Service;
using ChessHub.Application.UseCase;
using ChessHub.Application.ViewModel;
using ChessHub.Domain.Model;
using ChessHub.Domain.Repository;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ChessHub.Presentation.ViewModel
{
    public class LobbyViewModel : ILobbyViewModel
    {
        private readonly LeaveRoomUseCase _leaveRoomUseCase;
        private readonly CreateGameUseCase _createGameUseCase;
        private readonly PlayerReadyUseCase _playerReadyUseCase;
        private readonly JoinLobbyUseCase _joinLobbyUseCase;
        private readonly ISceneManagement _sceneManagement;
        private readonly ILobbyRepository _lobbyRepository;

        public string LobbyKey { get; set; }
        public string NewLobbyName { get; set; }
        public IReadOnlyList<PlayerModel> Players => _lobbyRepository.PlayerModels;

        public LobbyViewModel(LeaveRoomUseCase createRoomUseCase, CreateGameUseCase createGameUseCase,
            PlayerReadyUseCase playerReadyUseCase, ILobbyRepository lobbyRepository, ISceneManagement sceneManagement)
        {
            _leaveRoomUseCase = createRoomUseCase;
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
            var success = await _leaveRoomUseCase.ExecuteAsync(cancellationToken);
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