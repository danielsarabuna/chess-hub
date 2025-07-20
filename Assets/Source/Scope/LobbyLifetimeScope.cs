using Chess.Application;
using Chess.Application.Router;
using Chess.Application.UseCase;
using Chess.Application.ViewModel;
using Chess.Domain.Repository;
using Infrastructure.Repository;
using Presentation.Router;
using Presentation.View;
using Presentation.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scope
{
    public class LobbyLifetimeScope : ApplicationState
    {
        [SerializeField] private LobbyView mainMenuView;

        public override SessionState State => SessionState.Lobby;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(mainMenuView);

            builder.Register<ILobbyViewRouter, LobbyViewRouter>(Lifetime.Singleton);

            builder.Register<ILobbyViewModel, LobbyViewModel>(Lifetime.Transient);

            builder.Register<LeaveLobbyUseCase>(Lifetime.Singleton);
            builder.Register<CreateGameUseCase>(Lifetime.Singleton);
            builder.Register<PlayerReadyUseCase>(Lifetime.Singleton);

            builder.Register<ILobbyRepository, LobbyRepository>(Lifetime.Singleton);
        }
    }
}