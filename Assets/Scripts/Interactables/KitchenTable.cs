using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KitchenTable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _interactPoint;
    [SerializeField] Transform _slot;
    [SerializeField] Plate _initialPlate;

    ICarriable _carriable;

    public string DisplayName => "Kitchen Table";
    public Transform InteractPoint => _interactPoint;

    void Awake()
    {
        if (_initialPlate != null)
        {
            _carriable = _initialPlate;
            _carriable.OnPlaced(_slot);
        }
    }

    public async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (character == null)
        {
            Debug.LogWarning("[KitchenTable] Character is null");
            return;
        }

        if (character.IsHolding)
        {
            var carriable = character.CurrentCarriable;

            // 테이블에 아이템이 있고 그 아이템이 현재 든 아이템을 받을 수 있으면 전달
            if (_carriable != null && _carriable.CanReceive(carriable))
            {
                var item = character.PutDown();
                await _carriable.Receive(item, character, ct);
            }
            // ICarriable을 들고 있으면 테이블에 내려놓기
            else if (carriable is ICarriable)
            {
                var item = character.PutDown();
                _carriable = item;
                _carriable.OnPlaced(_slot);
                Debug.Log($"[KitchenTable] {item.GetType().Name} placed on table");
            }
        }
        else
        {
            // 캐릭터가 아무것도 안 들고 있으면 아이템 집기
            if (_carriable == null)
            {
                Debug.Log("[KitchenTable] No item to pick up");
                return;
            }

            character.PickUp(_carriable);
            _carriable.OnPickedUp();
            _carriable = null;
            Debug.Log("[KitchenTable] Item picked up from table");
        }

        await UniTask.CompletedTask;
    }
}
