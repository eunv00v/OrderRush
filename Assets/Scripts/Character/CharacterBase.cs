using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] Transform _itemSlot;

    public bool IsHolding => CurrentCarriable != null;
    public ICarriable CurrentCarriable { get; protected set; }
    public Transform ItemSlot => _itemSlot;

    public virtual void PickUp(ICarriable item)
    {
        if (item == null)
        {
            Debug.LogWarning($"[{gameObject.name}] Cannot pick up null item");
            return;
        }

        if (CurrentCarriable != null)
        {
            Debug.LogWarning($"[{gameObject.name}] Already holding an item: {CurrentCarriable}");
            return;
        }

        CurrentCarriable = item;

        Debug.Log($"[{gameObject.name}] Picked up: {item}");
    }

    public virtual ICarriable PutDown()
    {
        if (CurrentCarriable == null)
        {
            Debug.LogWarning($"[{gameObject.name}] No item to put down");
            return null;
        }

        var item = CurrentCarriable;
        CurrentCarriable = null;
        Debug.Log($"[{gameObject.name}] Put down: {item}");
        return item;
    }
}
