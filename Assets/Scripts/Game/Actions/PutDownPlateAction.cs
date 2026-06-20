using System.Threading;
using Cysharp.Threading.Tasks;

public class PutDownPlateAction : IGameAction
{
    private readonly StaffCharacter _staff;
    private readonly ServingCounter[] _servingCounters;
    private readonly Counter[] _kitchenCounters;

    public PutDownPlateAction(StaffCharacter staff, ServingCounter[] servingCounters, Counter[] kitchenCounters)
    {
        _staff = staff;
        _servingCounters = servingCounters;
        _kitchenCounters = kitchenCounters;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        if (!_staff.IsHolding)
            return;

        var serving = FindEmpty(_servingCounters);
        if (serving != null)
        {
            await PutDownAt(serving, ct);
            return;
        }

        var kitchen = FindEmpty(_kitchenCounters);
        if (kitchen != null)
        {
            await PutDownAt(kitchen, ct);
            return;
        }

        var anchor = FirstOrNull(_servingCounters);
        if (anchor == null)
            return;

        await new MoveAction(_staff.Mover, anchor.transform.position, _staff.Animator).ExecuteAsync(ct);

        Counter freed = null;
        await UniTask.WaitUntil(() =>
        {
            freed = FindEmpty(_servingCounters);
            return freed != null;
        }, cancellationToken: ct);

        await PutDownAt(freed, ct);
    }

    private UniTask PutDownAt(Counter counter, CancellationToken ct)
    {
        return new InteractAction(_staff.Mover, counter, _staff, _staff.Animator).ExecuteAsync(ct);
    }

    private static Counter FindEmpty(Counter[] counters)
    {
        if (counters == null)
            return null;

        foreach (var counter in counters)
        {
            if (counter != null && !counter.HasItem)
                return counter;
        }
        return null;
    }

    private static Counter FirstOrNull(Counter[] counters)
    {
        if (counters == null || counters.Length == 0)
            return null;
        return counters[0];
    }
}
