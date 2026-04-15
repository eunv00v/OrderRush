using System.Threading;
using Cysharp.Threading.Tasks;

public class OrderAction : IGameAction
{
    private readonly CustomerCharacter _customer;
    private readonly RecipeData _recipe;
    private readonly IOrderService _orderService;

    public OrderAction(CustomerCharacter customer, RecipeData recipe, IOrderService orderService)
    {
        _customer = customer;
        _recipe = recipe;
        _orderService = orderService;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        var order = _orderService.AddOrder(_recipe);
        _customer.AssignOrder(order);
        await UniTask.CompletedTask;
    }
}
