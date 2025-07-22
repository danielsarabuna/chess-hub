using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ChessHub.Application.Service
{
    public interface ISceneManagement
    {
        UniTask LoadSceneAsync(string name, LoadSceneMode mode);
        UniTask UploadSceneAsync(string name);
    }
}