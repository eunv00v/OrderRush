using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MoveState : ICharacterState
{
    readonly ICharacterStateMachine _stateMachine;
    readonly IdleState _idleState;

    NavMeshMover _mover;
    Vector3 _destination;
    ICharacterState _nextState;
    CancellationTokenSource _cts;

    public MoveState(ICharacterStateMachine stateMachine, IdleState idleState)
    {
        _stateMachine = stateMachine;
        _idleState = idleState;
    }

    public void SetMover(NavMeshMover mover)
    {
        _mover = mover;
    }

    public void SetDestination(Vector3 destination, ICharacterState nextState = null)
    {
        _destination = destination;
        _nextState = nextState ?? _idleState;
    }

    public void Enter()
    {
        Debug.Log($"Move 상태 진입 - 목적지: {_destination}");
        _cts = new CancellationTokenSource();
        MoveAsync().Forget();
    }

    public void Exit()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
        _mover.Stop();
    }

    public void Update()
    {
    }

    async UniTaskVoid MoveAsync()
    {
        try
        {
            await _mover.MoveToAsync(_destination, _cts.Token);
            _stateMachine.ChangeState(_nextState);
        }
        catch (System.OperationCanceledException)
        {
            // 이동 취소됨
        }
    }
}
