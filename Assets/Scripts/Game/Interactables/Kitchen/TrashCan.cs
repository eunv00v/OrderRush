using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TrashCan : InteractableBase
{

    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (!character.IsHolding) return;

        var carriable = await character.PutDown();
        if (carriable != null)
        {
            var component = carriable as Component;
            Destroy(component.gameObject);
        }

    }
}