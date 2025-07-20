using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Chess.Application.Service
{
    public interface ISceneManagement
    {
        UniTask LoadSceneAsync(string name, LoadSceneMode mode);
        UniTask UploadSceneAsync(string name);
    }
}