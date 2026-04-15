using System.Threading;
using Cysharp.Threading.Tasks;

public class SitAction : IGameAction
{
    private readonly CustomerCharacter _customer;
    private readonly DiningSeat _seat;

    public SitAction(CustomerCharacter customer, DiningSeat seat)
    {
        _customer = customer;
        _seat = seat;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        _customer.transform.position = _seat.SitPoint.position;
        _customer.transform.rotation = _seat.SitPoint.rotation;
        _seat.SeatCustomer(_customer);
        await UniTask.CompletedTask;
    }
}
