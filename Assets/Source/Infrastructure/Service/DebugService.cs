using ChessHub.Application.Service;
using UnityEngine;

namespace ChessHub.Infrastructure.Service
{
    public sealed class DebugService : IDebugService
    {
        private string _logFilePath;

        public void Log(string message)
        {
            Debug.Log($"[LOG]: {message}");
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning($"[WARNING]: {message}");
        }

        public void LogError(string message)
        {
            Debug.LogError($"[ERROR]: {message}");
        }
    }
}