using System.Collections.Generic;
using System.Linq;
using Chess.Application.Service;
using Chess.Domain.Model;
using VContainer;

namespace Infrastructure.Service
{
    using MessagePipe;
    using Photon.Pun;
    using Photon.Realtime;
    using UnityEngine;

    public class PhotonMultiplayerService : MonoBehaviourPunCallbacks, IUnityMultiplayerService
    {
        [SerializeField] private string gameVersion = "1.0";

        private IPublisher<ConnectedToServerMessage> _connectedPublisher;
        private IPublisher<JoinedRoomMessage> _joinedRoomPublisher;
        private IPublisher<MultiplayerErrorMessage> _errorPublisher;
        private IPublisher<PlayerLeftRoomMessage> _playerLeftRoomPublisher;
        private IPublisher<PlayerEnteredMessage> _playerEnteredPublisher;
        private List<LobbyModel> _lobbies = new();
        public IReadOnlyList<LobbyModel> GetAvailableLobbies => _lobbies;

        public string PlayerName => PhotonNetwork.LocalPlayer.NickName;

        [Inject]
        private void Construct(IPublisher<ConnectedToServerMessage> connectedPublisher,
            IPublisher<JoinedRoomMessage> joinedRoomPublisher,
            IPublisher<MultiplayerErrorMessage> errorPublisher,
            IPublisher<PlayerLeftRoomMessage> playerLeftRoomPublisher,
            IPublisher<PlayerEnteredMessage> playerEnteredPublisher)
        {
            _connectedPublisher = connectedPublisher;
            _joinedRoomPublisher = joinedRoomPublisher;
            _errorPublisher = errorPublisher;
            _playerLeftRoomPublisher = playerLeftRoomPublisher;
            _playerEnteredPublisher = playerEnteredPublisher;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void SetPlayerName(string value)
        {
            PhotonNetwork.LocalPlayer.NickName = value;
        }

        public void ConnectToServer()
        {
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom(string roomName)
        {
            var options = new RoomOptions { MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveLobby();
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Photon server.");
            _connectedPublisher.Publish(new ConnectedToServerMessage { ServerVersion = gameVersion });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");
            _joinedRoomPublisher.Publish(new JoinedRoomMessage { RoomName = PhotonNetwork.CurrentRoom.Name });
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Failed to join room: {message}");
            _errorPublisher.Publish(new MultiplayerErrorMessage { ErrorMessage = message });
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning($"Disconnected from Photon. Cause: {cause}");
            _errorPublisher.Publish(new MultiplayerErrorMessage { ErrorMessage = $"Disconnected: {cause}" });
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            Debug.Log($"OnRoomListUpdate called. Room count: {roomList.Count}");

            _lobbies = roomList
                .Select(x => new LobbyModel($"{x.masterClientId}", x.Name, x.PlayerCount, x.PlayerCount))
                .ToList();

            foreach (var room in _lobbies)
            {
                Debug.Log(
                    $"Room Name: {room.LobbyName}, Master Client ID: {room.LobbyId}, Player Count: {room.CurrentPlayers}");
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.Log($"Player left room: {otherPlayer.NickName}");

            _playerLeftRoomPublisher.Publish(new PlayerLeftRoomMessage { PlayerName = otherPlayer.NickName });
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log($"Player entered room: {newPlayer.NickName}");

            _playerEnteredPublisher.Publish(new PlayerEnteredMessage { PlayerName = newPlayer.NickName });
        }
    }
}