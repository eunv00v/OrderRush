using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class LobbyPresenter : IStartable, IDisposable
{
    private readonly LobbyView _view;
    private readonly LifetimeScope _lifetimeScope;
    private readonly CompositeDisposable _disposable = new();

    public LobbyPresenter(LobbyView view, LifetimeScope lifetimeScope)
    {
        _view = view;
        _lifetimeScope = lifetimeScope;
    }


    public void Start()
    {
        _view.StartButton
            .OnClickAsObservable()
            .Subscribe(_ => OnStartButtonClicked())
            .AddTo(_disposable);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.UnloadSceneAsync("LobbyScene");

        using (LifetimeScope.EnqueueParent(_lifetimeScope.Parent))
        {
            SceneManager.LoadSceneAsync("GameplayScene", LoadSceneMode.Additive);
        }
    }

    public void Dispose() => _disposable.Dispose();
}