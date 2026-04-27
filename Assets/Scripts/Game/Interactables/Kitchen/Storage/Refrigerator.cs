using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class Refrigerator : InteractableBase
{
    [NotNull][SerializeField] IngredientData _ingredient;
    [NotNull][SerializeField] Transform _doorTransform;

    [SerializeField] int _quantity = -1;  // -1이면 무한

    public bool IsEmpty => _currentPlateIndex == 0;
    private SpawnFactory _factory;
    private int _currentPlateIndex;
    private bool _isDoorOpen = false;

    [Inject]
    public void Construct(SpawnFactory factory)
    {
        _factory = factory;
        _currentPlateIndex = _quantity;
    }

    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (character == null)
        {
            Debug.LogWarning("Character is null");
            return;
        }

        // 이미 들고 있으면 무시
        if (character.IsHolding)
        {
            Debug.Log($"{character.name} is already holding an item");
            return;
        }

        // 재료가 없으면 무시
        if (IsEmpty)
        {
            Debug.Log("No ingredients available");
            return;
        }

        // Prefab 유효성 검사
        if (_ingredient == null)
        {
            Debug.LogError("IngredientData or Prefab is not assigned!");
            return;
        }

        await OpenDoorAnimation();

        if (!IsEmpty)
        {
            // IngredientObject 생성
            var ingredientObject = await _factory.Create<IngredientObject>(PrefabKeys.GetPrefabPath(_ingredient.PrefabName));
            ingredientObject.SetData(_ingredient);
            ingredientObject.transform.SetParent(character.ItemSlot);
            ingredientObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            ingredientObject.transform.localScale = Vector3.one;

            // 캐릭터에게 전달
            character.PickUp(ingredientObject);

            // 수량 감소 (-1이면 무한)
            if (_quantity > 0)
            {
                _currentPlateIndex--;
            }
        }

    }

    private UniTask OpenDoorAnimation()
    {
        _isDoorOpen = true;
        _doorTransform.DOKill();
        var tcs = new UniTaskCompletionSource();
        _doorTransform.DOLocalRotate(new Vector3(0, -90, 0), 0.5f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => tcs.TrySetResult());
        return tcs.Task;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log($"[Refrigerator] OnTriggerExit - Object: {other.name}, Tag: {other.tag}, _isDoorOpen: {_isDoorOpen}");

        if (other.CompareTag("Player") && _isDoorOpen)
        {
            Debug.Log("[Refrigerator] Closing door animation");
            CloseDoorAnimation();
        }
    }



    private void CloseDoorAnimation()
    {
        _isDoorOpen = false;
        _doorTransform.DOKill();
        _doorTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutBack);
    }



}
