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
    public class MainMenuLifetimeScope : ApplicationState
    {
        [SerializeField] private MainMenuView mainMenuView;

        public override SessionState State => SessionState.MainMenu;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(mainMenuView);

            builder.Register<IMainMenuViewRouter, MainMenuViewRouter>(Lifetime.Singleton);

            builder.Register<IMainMenuViewModel, MainMenuViewModel>(Lifetime.Transient);

            builder.Register<CreateLobbyUseCase>(Lifetime.Singleton);
            builder.Register<JoinLobbyUseCase>(Lifetime.Singleton);

            builder.Register<ILobbyRepository, LobbyRepository>(Lifetime.Singleton);
        }

    }
}