using System;
using UniRx;
using VContainer.Unity;

public class HudPresenter : IStartable, IDisposable
{
    readonly LevelProgressModel _model;
    readonly HudView _hudView;
    readonly CompositeDisposable _disposable = new();

    public HudPresenter(LevelProgressModel model, HudView hudView)
    {
        _model = model;
        _hudView = hudView;
    }

    public void Start()
    {
        _model.RemainingTime
            .Subscribe(time => _hudView.SetTimeGauge(time / _model.TimeLimit))
            .AddTo(_disposable);

        _model.EarnedMoney
            .Subscribe(coin => _hudView.SetCoin(coin))
            .AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }

}
