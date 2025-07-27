using ChessHub.Application;
using ChessHub.Application.Router;
using ChessHub.Application.UseCase;
using ChessHub.Application.ViewModel;
using ChessHub.Domain.Repository;
using ChessHub.Infrastructure.Repository;
using ChessHub.Presentation.Router;
using ChessHub.Presentation.View;
using ChessHub.Presentation.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ChessHub.Scope
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

            builder.Register<LeaveRoomUseCase>(Lifetime.Singleton);
            builder.Register<CreateGameUseCase>(Lifetime.Singleton);
            builder.Register<PlayerReadyUseCase>(Lifetime.Singleton);

            builder.Register<ILobbyRepository, LobbyRepository>(Lifetime.Singleton);
        }
    }
}