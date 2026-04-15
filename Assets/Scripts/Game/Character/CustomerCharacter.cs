using UnityEngine;
using VContainer;

public class CustomerCharacter : CharacterBase
{
    public Order Order { get; private set; }
    public DiningSeat AssignedSeat { get; private set; }

    private IOrderService _orderService;

    [Inject]
    public void Construct(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public void GoToSeat(DiningSeat targetSeat, RecipeData recipe)
    {
        AssignedSeat = targetSeat;
        EnqueueAction(new MoveAction(_mover, targetSeat.InteractPoint.position));
        EnqueueAction(new SitAction(this, targetSeat));
        EnqueueAction(new OrderAction(this, recipe, _orderService));
    }

    public void AssignOrder(Order order)
    {
        Order = order;
    }
}
