using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StaffLeaveAction : IGameAction
{
    private readonly StaffCharacter _staff;
    private readonly Vector3 _exitPosition;
    private readonly NavMeshMover _mover;
    private readonly CharacterAnimator _animator;

    public StaffLeaveAction(StaffCharacter staff, Vector3 exitPosition, NavMeshMover mover, CharacterAnimator animator)
    {
        _staff = staff;
        _exitPosition = exitPosition;
        _mover = mover;
        _animator = animator;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        if (_staff == null || _staff.gameObject == null)
            return;

        try
        {
            await new MoveAction(_mover, _exitPosition, _animator).ExecuteAsync(ct);
        }
        finally
        {
            if (_staff != null && _staff.gameObject != null)
                Object.Destroy(_staff.gameObject);
        }
    }
}
