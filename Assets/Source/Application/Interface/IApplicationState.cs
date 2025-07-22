namespace ChessHub.Application
{
    public enum SessionState
    {
        MainMenu,
        Lobby,
        Game,
    }

    public interface IApplicationState
    {
        bool Persists { get; }
        SessionState State { get; }
    }
}