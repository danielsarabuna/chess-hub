using ChessHub.Application;
using ChessHub.Application.Router;
using ChessHub.Application.Service;
using ChessHub.Application.ViewModel;
using ChessHub.Infrastructure.Service;
using MessagePipe;
using ChessHub.Presentation.Router;
using ChessHub.Presentation.View;
using ChessHub.Presentation.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scope
{
    public class StartupLifetimeScope : LifetimeScope
    {
        [SerializeField] private LoadingView loadingView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponent(loadingView);
            builder.Register<ILoadingViewModel, LoadingViewModel>(Lifetime.Singleton);
            builder.Register<ILoadingViewRouter, LoadingViewRouter>(Lifetime.Singleton);

            builder.Register<ISceneManagement, SceneManagement>(Lifetime.Singleton);
            builder.Register<IDebugService, DebugService>(Lifetime.Singleton);

            var options = builder.RegisterMessagePipe();

            builder.RegisterMessageBroker<ConnectedToServerMessage>(options);
            builder.RegisterMessageBroker<JoinedRoomMessage>(options);
            builder.RegisterMessageBroker<LeaveRoomMessage>(options);
            builder.RegisterMessageBroker<PlayerLeftRoomMessage>(options);
            builder.RegisterMessageBroker<PlayerEnteredMessage>(options);
            builder.RegisterMessageBroker<MultiplayerErrorMessage>(options);

            builder.RegisterComponentOnNewGameObject<PhotonMultiplayerService>(Lifetime.Singleton,
                    "MultiplayerService")
                .As<IUnityMultiplayerService>()
                .AsSelf();

            builder.RegisterEntryPoint<MultiplayerManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}