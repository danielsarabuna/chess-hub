using ExitGames.Client.Photon;
using Photon.Pun;
using MessagePipe;
using Photon.Realtime;

namespace ChessHub.Infrastructure
{
    public class ChessNetworkManager
    {
        private readonly IPublisher<string> _eventPublisher;

        public ChessNetworkManager(IPublisher<string> eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void SendMove(string moveData)
        {
            PhotonNetwork.RaiseEvent(0, moveData, RaiseEventOptions.Default, SendOptions.SendReliable);
            _eventPublisher.Publish($"Move sent: {moveData}");
        }
    }
}