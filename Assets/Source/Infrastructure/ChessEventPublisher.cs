using Chess.Domain;
using MessagePipe;

namespace Chess.Infrastructure
{
    public class ChessEventPublisher
    {
        private readonly IPublisher<string> _publisher;

        public ChessEventPublisher(IPublisher<string> publisher)
        {
            _publisher = publisher;
        }

        public void NotifyTurnChange(PlayerColor nextTurn)
        {
            var message = $"Turn changed to: {nextTurn}";
            _publisher.Publish(message);
            UnityEngine.Debug.Log(message);
        }
    }
}