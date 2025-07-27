using System.Collections.Generic;
using System.Linq;
using ChessHub.Application.Service;
using ChessHub.Domain.Model;
using VContainer;

namespace ChessHub.Infrastructure.Service
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
        private readonly Dictionary<string, LobbyModel> _cachedRoomList = new();
        private readonly Dictionary<string, PlayerModel> _cachedPlayerList = new();
        public IReadOnlyList<LobbyModel> LobbyModels => _cachedRoomList.Values.ToList();

        public IReadOnlyList<PlayerModel> PlayerModels => _cachedPlayerList.Values.ToList();

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

        public void ConnectToServer()
        {
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.LocalPlayer.NickName = $"Player {Random.Range(1000, 10000)}";
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom(string roomName)
        {
            var options = new RoomOptions { MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
        }

        public void JoinRoom(string roomName)
        {
            if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
            PhotonNetwork.JoinRoom(roomName);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
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

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning($"Disconnected from Photon. Cause: {cause}");
            _errorPublisher.Publish(new MultiplayerErrorMessage { ErrorMessage = $"Disconnected: {cause}" });
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            Debug.Log("Created room.");
        }

        public override void OnJoinedLobby()
        {
            _cachedRoomList.Clear();
        }

        public override void OnLeftLobby()
        {
            _cachedRoomList.Clear();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");
            _cachedRoomList.Clear();
            foreach (var player in PhotonNetwork.PlayerList)
                _cachedPlayerList[player.NickName] = new PlayerModel(player.NickName);
            _joinedRoomPublisher.Publish(new JoinedRoomMessage { RoomName = PhotonNetwork.CurrentRoom.Name });
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Failed to create room: {message}");
            _errorPublisher.Publish(new MultiplayerErrorMessage { ErrorMessage = message });
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Failed to join room: {message}");
            _errorPublisher.Publish(new MultiplayerErrorMessage { ErrorMessage = message });
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            Debug.Log($"OnRoomListUpdate called. Room count: {roomList.Count}");
            UpdateCachedRoomList(roomList);
        }

        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            foreach (var roomInfo in roomList)
            {
                if (!roomInfo.IsOpen || !roomInfo.IsVisible || roomInfo.RemovedFromList)
                {
                    _cachedRoomList.Remove(roomInfo.Name);
                    continue;
                }

                _cachedRoomList[roomInfo.Name] = new LobbyModel($"{roomInfo.masterClientId}", roomInfo.Name,
                    roomInfo.PlayerCount, roomInfo.PlayerCount);
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.Log($"Player left room: {otherPlayer.NickName}");
            _cachedPlayerList.Remove(otherPlayer.NickName);
            _playerLeftRoomPublisher.Publish(new PlayerLeftRoomMessage { PlayerName = otherPlayer.NickName });
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log($"Player entered room: {newPlayer.NickName}");
            _cachedPlayerList[newPlayer.NickName] = new PlayerModel(newPlayer.NickName);
            _playerEnteredPublisher.Publish(new PlayerEnteredMessage { PlayerName = newPlayer.NickName });
        }
    }
}