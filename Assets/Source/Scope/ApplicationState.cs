using ChessHub.Application;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace ChessHub.Scope
{
    public abstract class ApplicationState : LifetimeScope, IApplicationState
    {
        public virtual bool Persists { get; } = false;
        public abstract SessionState State { get; }

        protected override void Awake()
        {
            name = "Temp";
            base.Awake();
        }

        private void Reset()
        {
            autoRun = false;
        }

        private void OnValidate()
        {
            autoRun = false;
        }

        private async void Start()
        {
            Build();
            await Initialize();
        }

        protected virtual UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}