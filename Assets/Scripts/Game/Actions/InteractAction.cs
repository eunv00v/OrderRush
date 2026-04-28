using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class InteractAction : IGameAction
{
    private readonly NavMeshMover _mover;
    private readonly IInteractable _target;
    private readonly CharacterBase _character;
    private readonly CharacterAnimator _animator;

    public InteractAction(NavMeshMover mover, IInteractable target, CharacterBase character, CharacterAnimator animator)
    {
        _mover = mover;
        _target = target;
        _character = character;
        _animator = animator;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        var interactPoint = _target.InteractPoint.position;

        // InteractPoint로 이동 가능한지 확인
        if (!NavMesh.SamplePosition(interactPoint, out var navHit, 2.0f, NavMesh.AllAreas))
        {
            //     Debug.LogWarning($"[InteractAction] Cannot navigate to {_target.GetType().Name} at {interactPoint}");
            return;
        }

        try
        {
            // InteractPoint로 이동
            _target.SetHighlight(true);
            _animator.SetSpeed(1f);
            await _mover.MoveToAsync(navHit.position, ct);
            _animator.SetSpeed(0f);

            // InteractPoint의 방향을 따라 회전
            _character.transform.rotation = _target.InteractPoint.rotation;

            // 상호작용 실행
            await _target.InteractAsync(_character, ct);
        }
        finally
        {
            _target.SetHighlight(false);
        }
    }
}
