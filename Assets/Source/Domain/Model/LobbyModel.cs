namespace ChessHub.Domain.Model
{
    public class LobbyModel
    {
        public string LobbyId { get; }
        public string LobbyName { get; }
        public int MaxPlayers { get; }
        public int CurrentPlayers { get; }

        public LobbyModel(string lobbyId, string lobbyName, int maxPlayers, int currentPlayers)
        {
            LobbyId = lobbyId;
            LobbyName = lobbyName;
            MaxPlayers = maxPlayers;
            CurrentPlayers = currentPlayers;
        }
    }
}