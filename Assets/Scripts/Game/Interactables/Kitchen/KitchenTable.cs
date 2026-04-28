using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KitchenTable : InteractableBase
{

    [NotNull][SerializeField] Transform _slot;
    [SerializeField] Plate _initialPlate;

    ICarriable _carriable;

    void Awake()
    {
        if (_initialPlate != null)
        {
            _carriable = _initialPlate;
            _carriable.OnPutDown(_slot);
        }
    }

    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (character == null) return;


        if (character.IsHolding && _carriable != null)
        {
            // 캐릭터(Plate) + 테이블(Ingredient) → 접시에 재료 올리기
            if (character.CurrentCarriable.TryPlaceOnto(_carriable))
            {
                await character.PickUp(character.CurrentCarriable);
                _carriable = null;
            }
            // 캐릭터(Ingredient) + 테이블(Plate) → 접시에 재료 올리기
            else if (_carriable.TryPlaceOnto(character.CurrentCarriable))
            {
                await character.PickUp(_carriable);
                _carriable = null;
            }
        }
        else if (character.IsHolding && _carriable == null)
        {
            var item = await character.PutDown();
            _carriable = item;
            _carriable.OnPutDown(_slot);
        }
        else if (character.IsHolding == false && _carriable != null)
        {
            await character.PickUp(_carriable);
            _carriable = null;
        }


        await UniTask.CompletedTask;
    }
}
