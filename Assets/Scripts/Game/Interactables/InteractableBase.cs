using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    [NotNull][SerializeField] protected InteractableHighlight _platesHighlight;
    [NotNull][SerializeField] protected Transform _interactPoint;

    public Transform InteractPoint => _interactPoint;

    virtual public UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        return UniTask.CompletedTask;
    }

    public void SetHighlight(bool highlight)
    {
        _platesHighlight.SetHighlight(highlight);
    }



}
