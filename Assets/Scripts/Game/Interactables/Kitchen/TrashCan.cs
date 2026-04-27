using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TrashCan : InteractableBase
{

    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        if (!character.IsHolding) return;

        if (character.PutDown() is MonoBehaviour item)
        {
            await UniTask.Delay(100, cancellationToken: ct);
            Destroy(item.gameObject);
        }

        await UniTask.CompletedTask;
    }
}