using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 캐릭터가 들고 다닐 수 있는 오브젝트 인터페이스
/// </summary>
public interface ICarriable
{
    void OnPlaced(Transform slot);
    void OnPickedUp();
    bool CanReceive(ICarriable item);
    UniTask Receive(ICarriable item, CharacterBase character, CancellationToken ct);
}
