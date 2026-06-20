using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StaffIdleAction : IGameAction
{
    private readonly NavMeshMover _mover;
    private readonly Transform[] _waypoints;
    private int _lastIndex = -1;

    public StaffIdleAction(NavMeshMover mover, Transform[] waypoints)
    {
        _mover = mover;
        _waypoints = waypoints;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        if (_waypoints == null || _waypoints.Length == 0)
            return;

        while (!ct.IsCancellationRequested)
        {
            int index = GetNextIndex();
            await _mover.MoveToAsync(_waypoints[index].position, ct);
            await UniTask.Delay((int)(Random.Range(1f, 3f) * 1000), cancellationToken: ct);
        }
    }

    private int GetNextIndex()
    {
        if (_waypoints.Length == 1)
            return 0;

        int index;
        do { index = Random.Range(0, _waypoints.Length); }
        while (index == _lastIndex);
        _lastIndex = index;
        return index;
    }
}
