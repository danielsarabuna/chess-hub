using ChessHub.Application.Service;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ChessHub.Infrastructure.Service
{
    public class SceneManagement : ISceneManagement
    {
        public async UniTask LoadSceneAsync(string name, LoadSceneMode mode)
        {
            await SceneManager.LoadSceneAsync(name, mode).ToUniTask();
        }

        public async UniTask UploadSceneAsync(string name)
        {
            await SceneManager.UnloadSceneAsync(name).ToUniTask();
        }
    }
}