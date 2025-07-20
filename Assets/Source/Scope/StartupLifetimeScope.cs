using Chess.Application;
using Chess.Application.Router;
using Chess.Application.Service;
using Chess.Application.ViewModel;
using Infrastructure.Service;
using MessagePipe;
using Presentation.Router;
using Presentation.View;
using Presentation.ViewModel;
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