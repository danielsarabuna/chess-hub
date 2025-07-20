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