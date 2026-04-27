using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IInteractable
{
    Transform InteractPoint { get; }
    UniTask InteractAsync(CharacterBase character, CancellationToken ct);
    void SetHighlight(bool highlight);
}