using UnityEngine;
using UniRx;

public class CookingProgressPresenter : MonoBehaviour
{
    [SerializeField] CookingToolBase _tool;
    [SerializeField] CookingProgressView _view;

    CompositeDisposable _disposables = new();

    void Start()
    {
        // Progress 구독
        _tool.Model.Progress
            .Subscribe(progress => _view.SetProgress(progress))
            .AddTo(_disposables);

        // IsCooking 구독
        _tool.Model.IsCooking
            .Subscribe(isCooking => _view.SetVisible(isCooking))
            .AddTo(_disposables);
    }

    void OnDestroy()
    {
        _disposables?.Dispose();
    }
}
