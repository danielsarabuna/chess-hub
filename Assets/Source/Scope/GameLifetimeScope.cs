using ChessHub.Application;
using ChessHub.Domain;
using ChessHub.Infrastructure;
using MessagePipe;
using VContainer;

namespace ChessHub.Scope
{
    public class GameLifetimeScope : ApplicationState
    {
        public override SessionState State => SessionState.Game;

        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<string>(options);

            builder.Register<ChessNetworkManager>(Lifetime.Singleton);
            builder.Register<ChessEventPublisher>(Lifetime.Singleton);
            builder.Register<ChessGameSynchronizer>(Lifetime.Singleton);
            builder.Register<ChessBoard>(Lifetime.Singleton);

            builder.Register<GameManager>(Lifetime.Singleton);
            builder.Register<PlayerTurnManager>(Lifetime.Singleton);
            builder.Register<IMoveValidator, MoveValidator>(Lifetime.Singleton);
        }
    }
}