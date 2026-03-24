using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _interactPoint;
    public string DisplayName => gameObject.name;
    public Transform InteractPoint => _interactPoint;

    public async UniTask InteractAsync(CancellationToken ct)
    {
        Debug.Log($"[{DisplayName}] Interaction started");
        await UniTask.Delay(1000, cancellationToken: ct);
        Debug.Log($"[{DisplayName}] Interaction completed");
    }

    void OnDrawGizmos()
    {
        if (_interactPoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_interactPoint.position, 0.2f);
    }
}
