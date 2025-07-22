using System;
using System.Collections.Generic;
using System.Threading;
using ChessHub.Application.Router;
using ChessHub.Application.Service;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace ChessHub.Application
{
    using UnityEngine;
    using MessagePipe;

    public class MultiplayerManager : IStartable, IDisposable
    {
        private readonly IUnityMultiplayerService _multiplayerService;
        private readonly ISceneManagement _sceneManagement;
        private readonly ILoadingViewRouter _loadingViewRouter;
        private readonly DisposableBagBuilder _disposableBagBuilder;
        private CancellationTokenSource _tokenSource;

        [Inject]
        private MultiplayerManager(IUnityMultiplayerService service,
            ISceneManagement sceneManagement,
            ILoadingViewRouter loadingViewRouter,
            ISubscriber<ConnectedToServerMessage> connectedSubscriber,
            ISubscriber<JoinedRoomMessage> joinedRoomSubscriber,
            ISubscriber<MultiplayerErrorMessage> errorSubscriber)
        {
            _multiplayerService = service;
            _sceneManagement = sceneManagement;
            _loadingViewRouter = loadingViewRouter;
            _disposableBagBuilder = DisposableBag.CreateBuilder();

            connectedSubscriber.Subscribe(OnConnected).AddTo(_disposableBagBuilder);
            joinedRoomSubscriber.Subscribe(OnRoomJoined).AddTo(_disposableBagBuilder);
            errorSubscriber.Subscribe(OnError).AddTo(_disposableBagBuilder);

        }

        public void Start()
        {
            _multiplayerService.ConnectToServer();
        }

        private void OnConnected(ConnectedToServerMessage message)
        {
            Debug.Log($"Connected to server. Version: {message.ServerVersion}");
            TransitionToMainMenu();
        }

        private void OnRoomJoined(JoinedRoomMessage message)
        {
            Debug.Log($"Successfully joined room: {message.RoomName}");
        }

        private void OnError(MultiplayerErrorMessage message)
        {
            Debug.LogError($"Multiplayer Error: {message.ErrorMessage}");
            TransitionToError();
        }

        private async void TransitionToMainMenu()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();

            var taskList = new List<Func<UniTask>>
            {
                () => _sceneManagement.LoadSceneAsync("Boot", LoadSceneMode.Single),
                () => _sceneManagement.LoadSceneAsync("MainMenu", LoadSceneMode.Single)
            };
            await _loadingViewRouter.Execute(taskList, _tokenSource.Token);
        }

        private async void TransitionToError()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();

            var taskList = new List<Func<UniTask>>
            {
                () => _sceneManagement.LoadSceneAsync("Boot", LoadSceneMode.Single),
                () => _sceneManagement.LoadSceneAsync("Error", LoadSceneMode.Single)
            };
            await _loadingViewRouter.Execute(taskList, _tokenSource.Token);
        }

        public void Dispose()
        {
            _disposableBagBuilder.Clear();
            Debug.Log("Disposing multiplayer manager");
        }
    }
}