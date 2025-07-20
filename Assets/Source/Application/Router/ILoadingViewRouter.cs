using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Chess.Application.Router
{
    public interface ILoadingViewRouter : IViewRouter
    {
        public UniTask Execute(IList<Func<UniTask>> taskList, CancellationToken token);
    }
}