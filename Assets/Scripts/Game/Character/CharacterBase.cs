using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [NotNull][SerializeField] protected Transform _itemSlot;
    [NotNull][SerializeField] protected ActionExecutor _actionExecutor;
    [NotNull][SerializeField] protected NavMeshMover _mover;
    [NotNull][SerializeField] protected CharacterAnimator _animator;

    public bool IsHolding => CurrentCarriable != null;
    public ICarriable CurrentCarriable { get; protected set; }
    public Transform ItemSlot => _itemSlot;
    public bool IsExecuting => _actionExecutor.IsExecuting();

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
        item.OnPickedUp(ItemSlot);
        _animator.TriggerPickUp();

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
        _animator.TriggerPutDown();
        Debug.Log($"[{gameObject.name}] Put down: {item}");
        return item;
    }

    public void StartWorking()
    {
        _animator.SetWorking(true);
    }

    public void StopWorking()
    {
        _animator.SetWorking(false);
    }

    public void EnqueueAction(IGameAction action)
    {
        _actionExecutor.Enqueue(action);
    }

    public void ClearActions()
    {
        _actionExecutor.Clear();
    }

    public void EnableNavMeshAgent()
    {
        if (_mover != null)
        {
            _mover.EnableAgent();
        }
    }

    public void DisableNavMeshAgent()
    {
        if (_mover != null)
        {
            _mover.DisableAgent();
        }
    }

    public void WarpTo(Vector3 position)
    {
        if (_mover != null)
        {
            _mover.Warp(position);
        }
    }
}
