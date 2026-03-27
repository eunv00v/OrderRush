using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

/// <summary>
/// 조리 도구 베이스 클래스 (Pan, Oven, Grill 등)
/// </summary>
public abstract class CookingToolBase : MonoBehaviour, IInteractable
{
    [Header("Interaction")]
    [SerializeField] protected Transform _interactPoint;
    [SerializeField] protected Transform _ingredientSlot;
    [SerializeField] protected Canvas _canvas;


    protected IngredientContext _currentIngredient;
    private CookingProcess _cookingProcess;
    public CookingToolModel Model { get; private set; }

    public abstract string DisplayName { get; }
    public Transform InteractPoint => _interactPoint;
    public IngredientContext CurrentIngredient => _currentIngredient;
    public bool IsOccupied => _currentIngredient != null;
    public bool IsCooking => _cookingProcess != null;

    protected virtual void Awake()
    {
        _canvas.worldCamera = Camera.main;
        Model = new CookingToolModel();
    }



    protected virtual void Start()
    {
    }

    protected virtual void OnDestroy()
    {
        StopCookingTimer();
        Model?.Dispose();
    }

    public virtual void PlaceIngredient(IngredientData ingredient)
    {
        if (IsOccupied)
        {
            Debug.LogWarning($"[{DisplayName}] 이미 재료가 있습니다.");
            return;
        }

        _currentIngredient = new IngredientContext(ingredient);
        Model.IngredientState.Value = _currentIngredient.State;
        Debug.Log($"[{DisplayName}] 재료 배치: {_currentIngredient}");
    }

    public virtual IngredientContext RemoveIngredient()
    {
        if (!IsOccupied)
        {
            Debug.LogWarning($"[{DisplayName}] 재료가 없습니다.");
            return null;
        }

        var ingredientModel = _currentIngredient;
        _currentIngredient = null;
        StopCookingTimer();
        Model.IngredientState.Value = global::IngredientState.Raw;
        Debug.Log($"[{DisplayName}] 재료 제거: {ingredientModel}");
        return ingredientModel;
    }

    protected void StartCookingTimer(CookableAbility ability)
    {
        if (_cookingProcess != null)
        {
            StopCookingTimer();
        }

        _cookingProcess = new CookingProcess
        {
            Ability = ability,
            Cts = new CancellationTokenSource(),
            ElapsedTime = 0f
        };

        Model.IsCooking.Value = true;
        CookAsync(_cookingProcess).Forget();
    }

    protected void StopCookingTimer()
    {
        Debug.Log($"[StopCookingTimer]초");
        if (_cookingProcess != null)
        {
            _cookingProcess.Cts?.Cancel();
            _cookingProcess.Cts?.Dispose();
            _cookingProcess = null;
            Model.IsCooking.Value = false;
            Model.Progress.Value = 0f;
        }
    }

    public float GetProgress()
    {
        return _cookingProcess?.Progress ?? 0f;
    }

    private async UniTask CookAsync(CookingProcess process)
    {
        var ct = process.Cts.Token;

        try
        {
            while (process.ElapsedTime < process.Ability.CookDuration)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                process.ElapsedTime += Time.deltaTime;
                Model.Progress.Value = process.Progress;
            }

            if (IsOccupied)
            {
                _currentIngredient.State = process.Ability.ResultState;
                Model.IngredientState.Value = _currentIngredient.State;
                Debug.Log($"[{DisplayName}] 조리 완료: {_currentIngredient}");
            }

            if (process.Ability.OverdueDelay > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(process.Ability.OverdueDelay), cancellationToken: ct);
                if (IsOccupied)
                {
                    _currentIngredient.State = process.Ability.ResultState;
                    Model.IngredientState.Value = _currentIngredient.State;
                    Debug.LogWarning($"[{DisplayName}] 과조리됨!");
                }
            }

        }
        catch (OperationCanceledException)
        {
            Debug.Log($"[CookAsync]: 취소댐");
        }
        finally
        {
            _cookingProcess = null;
            Model.IsCooking.Value = false;
            Model.Progress.Value = 0f;
        }
    }

    public abstract UniTask InteractAsync(CharacterBase character, CancellationToken ct);
}
