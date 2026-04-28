using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public abstract class CookingToolBase : InteractableBase
{

    [NotNull][SerializeField] protected Transform _ingredientSlot;
    [NotNull][SerializeField] protected Canvas _canvas;
    [NotNull][SerializeField] protected CookingProgressView _progressView;

    public IngredientObject CurrentIngredientObject { get; protected set; }
    public IngredientData CurrentIngredientData => CurrentIngredientObject != null ? CurrentIngredientObject.Data : null;
    public bool HasIngredient => CurrentIngredientObject != null;

    public bool IsCooking { get; protected set; }
    private SpawnFactory _factory;

    protected virtual void Awake()
    {
        _canvas.worldCamera = Camera.main;
    }


    [Inject]
    public void Construct(SpawnFactory factory)
    {
        _factory = factory;
    }

    protected virtual void OnDestroy()
    {
        StopCooking();
    }

    public virtual void PlaceIngredient(IngredientData ingredient, IngredientObject ingredientObject)
    {
        if (HasIngredient)
        {
            Debug.LogWarning("이미 재료가 있습니다.");
            return;
        }

        CurrentIngredientObject = ingredientObject;
        ingredientObject.SetData(ingredient);
        Debug.Log($"재료 배치: {ingredient.IngredientName}");
    }

    public virtual void RemoveIngredient()
    {
        if (!HasIngredient)
        {
            Debug.LogWarning("재료가 없습니다.");
            return;
        }

        var ingredientData = CurrentIngredientObject.Data;
        CurrentIngredientObject = null;
        StopCooking();
        Debug.Log($"재료 제거: {ingredientData.IngredientName}");

    }


    protected virtual bool CanPlaceIngredient(IngredientData ingredient)
    {
        return true;
    }


    protected virtual async void StartCooking()
    {
        await UniTask.CompletedTask;
    }

    protected virtual void StopCooking()
    {
        _progressView.SetVisible(false);
        IsCooking = false;
    }

    protected void UpdateProgress(float elapsedTime, float duration)
    {
        float progress = elapsedTime / duration;
        _progressView.SetProgress(progress);
    }


    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {

        if (character.IsHolding && HasIngredient)
        {
            // 캐릭터(Plate) + 테이블(Ingredient) → 접시에 재료 올리기
            if (character.CurrentCarriable.TryPlaceOnto(CurrentIngredientObject))
            {
                await character.PickUp(character.CurrentCarriable);
                RemoveIngredient();
            }
            // 캐릭터(Ingredient) + 테이블(Plate) → 접시에 재료 올리기
            else if (CurrentIngredientObject.TryPlaceOnto(character.CurrentCarriable))
            {
                await character.PickUp(CurrentIngredientObject);
                RemoveIngredient();
            }
        }
        else if (character.IsHolding && !HasIngredient)
        {
            if (CanPlaceIngredient(CurrentIngredientObject.Data))
            {
                await character.PutDown();
                CurrentIngredientObject.transform.SetParent(_ingredientSlot);
                CurrentIngredientObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                PlaceIngredient(CurrentIngredientObject.Data, CurrentIngredientObject);
                StartCooking();
            }
            else
            {
                Debug.LogWarning("이 도구에 올릴 수 없는 재료입니다.");
            }
        }
        else if (!character.IsHolding && HasIngredient)
        {
            await character.PickUp(CurrentIngredientObject);
            RemoveIngredient();
        }

        await UniTask.CompletedTask;
    }

    protected async UniTask CompleteTransition(IngredientTransition transition)
    {
        Destroy(CurrentIngredientObject.gameObject);
        CurrentIngredientObject = await _factory.Create<IngredientObject>(PrefabKeys.GetPrefabPath(transition.Result.PrefabName));
        CurrentIngredientObject.SetData(transition.Result);
        CurrentIngredientObject.transform.SetParent(_ingredientSlot);
        CurrentIngredientObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

    }
}
