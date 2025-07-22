using System;
using System.Linq;
using ChessHub.Application.Service;
using ChessHub.Domain.Model;
using ChessHub.Domain.Repository;
using MessagePipe;

namespace ChessHub.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public class LobbyRepository : ILobbyRepository, IDisposable
    {
        private readonly IUnityMultiplayerService _multiplayerService;
        private readonly DisposableBagBuilder _disposableBagBuilder;
        private List<PlayerModel> _playerList = new();
        private UniTaskCompletionSource<bool> _completionSource;
        public IReadOnlyList<PlayerModel> GetPlayerList => _playerList;

        private LobbyRepository(IUnityMultiplayerService multiplayerService,
            ISubscriber<JoinedRoomMessage> joinedRoomSubscriber,
            ISubscriber<LeaveRoomMessage> leaveRoomSubscriber,
            ISubscriber<PlayerEnteredMessage> playerEnteredSubscriber,
            ISubscriber<PlayerLeftRoomMessage> playerLeftRoomSubscriber,
            ISubscriber<MultiplayerErrorMessage> errorSubscriber)

        {
            _multiplayerService = multiplayerService;
            _disposableBagBuilder = DisposableBag.CreateBuilder();

            joinedRoomSubscriber.Subscribe(OnRoomJoined).AddTo(_disposableBagBuilder);
            leaveRoomSubscriber.Subscribe(OnLeaveRoom).AddTo(_disposableBagBuilder);
            playerEnteredSubscriber.Subscribe(OnPlayerEntered).AddTo(_disposableBagBuilder);
            playerLeftRoomSubscriber.Subscribe(OnPlayerLeftRoom).AddTo(_disposableBagBuilder);
            errorSubscriber.Subscribe(OnError).AddTo(_disposableBagBuilder);
        }

        public async UniTask<bool> CreateLobbyAsync(string lobbyName, CancellationToken cancellationToken)
        {
            _completionSource = new UniTaskCompletionSource<bool>();
            _multiplayerService.CreateRoom(lobbyName);
            return await _completionSource.Task;
        }

        public async UniTask<bool> JoinLobbyAsync(string lobbyId, CancellationToken cancellationToken)
        {
            _completionSource = new UniTaskCompletionSource<bool>();
            _multiplayerService.JoinRoom(lobbyId);
            return await _completionSource.Task;
        }

        public async UniTask<bool> LeaveLobbyAsync(CancellationToken cancellationToken)
        {
            _completionSource = new UniTaskCompletionSource<bool>();
            _multiplayerService.LeaveRoom();
            return await _completionSource.Task;
        }

        private void OnRoomJoined(JoinedRoomMessage message)
        {
            _completionSource?.TrySetResult(true);
            _completionSource = null;
        }

        private void OnLeaveRoom(LeaveRoomMessage message)
        {
            _playerList.Clear();
        }

        private void OnPlayerEntered(PlayerEnteredMessage message)
        {
            _playerList.Add(new PlayerModel(message.PlayerName));
        }

        private void OnPlayerLeftRoom(PlayerLeftRoomMessage message)
        {
            _playerList = _playerList
                .Where(x => x.Nickname == message.PlayerName)
                .ToList();
        }

        private void OnError(MultiplayerErrorMessage message)
        {
            _completionSource?.TrySetResult(false);
            _completionSource = null;
        }

        public void Dispose()
        {
            _disposableBagBuilder?.Clear();
        }
    }
}