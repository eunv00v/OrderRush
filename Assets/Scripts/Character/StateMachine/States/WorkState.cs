using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WorkState : ICharacterState
{
    readonly ICharacterStateMachine _stateMachine;
    readonly IdleState _idleState;

    IInteractable _target;
    CancellationTokenSource _cts;

    public WorkState(ICharacterStateMachine stateMachine, IdleState idleState)
    {
        _stateMachine = stateMachine;
        _idleState = idleState;
    }

    public void SetTarget(IInteractable target)
    {
        _target = target;
    }

    public void Enter()
    {
        Debug.Log($"Work 상태 진입 - 대상: {_target?.DisplayName}");
        _cts = new CancellationTokenSource();
        InteractAsync().Forget();
    }

    public void Exit()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public void Update()
    {
    }

    async UniTaskVoid InteractAsync()
    {
        if (_target == null)
        {
            _stateMachine.ChangeState(_idleState);
            return;
        }

        try
        {
            await _target.InteractAsync(_cts.Token);
            _stateMachine.ChangeState(_idleState);
        }
        catch (System.OperationCanceledException)
        {
            // 상호작용 취소됨
        }
    }
}
