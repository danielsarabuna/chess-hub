namespace ChessHub.Application.Service
{
    public interface IDebugService
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}