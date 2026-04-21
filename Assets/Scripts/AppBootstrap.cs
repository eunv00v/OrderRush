using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class AppBootstrap : IAsyncStartable
{
    public async UniTask StartAsync(CancellationToken cancellation)
    {
        await SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive)
            .ToUniTask(cancellationToken: cancellation);
    }
}