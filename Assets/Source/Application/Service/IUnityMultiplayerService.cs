using System.Collections.Generic;
using Chess.Domain.Model;

namespace Chess.Application.Service
{
    public struct ConnectedToServerMessage
    {
        public string ServerVersion;
    }

    public struct JoinedRoomMessage
    {
        public string RoomName;
    }

    public struct LeaveRoomMessage
    {
    }

    public struct MultiplayerErrorMessage
    {
        public string ErrorMessage;
    }

    public struct PlayerLeftRoomMessage
    {
        public string PlayerName;
    }

    public struct PlayerEnteredMessage
    {
        public string PlayerName;
    }

    public interface IUnityMultiplayerService
    {
        IReadOnlyList<LobbyModel> GetAvailableLobbies { get; }
        string PlayerName { get; }

        void SetPlayerName(string value);
        void ConnectToServer();
        void CreateRoom(string roomName);
        void JoinRoom(string roomName);
        void LeaveRoom();
        void Disconnect();
    }
}