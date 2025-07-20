using System.IO;
using System.Threading;
using Chess.Application.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Service
{
    public sealed class DebugService : IDebugService
    {
        private string _logFilePath;

        public async UniTask InitializeAsync(CancellationToken cancellation)
        {
            Debug.Log("Initializing Debug Service...");

#if UNITY_EDITOR
            _logFilePath = Path.Combine(Application.dataPath, "debug_log.txt");
#else
            _logFilePath = Path.Combine(Application.persistentDataPath, "debug_log.txt");
#endif

            if (!File.Exists(_logFilePath))
            {
                await File.Create(_logFilePath).DisposeAsync();
            }

            Debug.Log("Debug Service Initialized!");
            await UniTask.CompletedTask;
        }

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