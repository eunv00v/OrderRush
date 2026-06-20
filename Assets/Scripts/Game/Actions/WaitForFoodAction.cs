using System.Threading;
using Cysharp.Threading.Tasks;
using OrderRush.Services;

public class WaitForFoodAction : IGameAction
{
    private readonly CustomerCharacter _character;
    private readonly OrderIconFactory _orderIconFactory;
    private readonly IGameDataService _gameDataService;
    private OrderIconPresenter _orderIconPresenter;

    public WaitForFoodAction(CustomerCharacter character, OrderIconFactory orderIconFactory, IGameDataService gameDataService)
    {
        _character = character;
        _orderIconFactory = orderIconFactory;
        _gameDataService = gameDataService;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        if (_character == null || _character.gameObject == null)
            return;

        if (_character.OrderedRecipeID == -1)
            return;

        var recipe = _gameDataService.GetRecipeByID(_character.OrderedRecipeID);
        if (recipe == null)
            return;

        try
        {
            _orderIconPresenter = _orderIconFactory.Create(_character.transform, new UnityEngine.Vector3(0, 1.5f, 0));
            _orderIconPresenter.SetIcon(recipe.Icon);
            _orderIconPresenter.Show();

            await UniTask.WaitUntilCanceled(ct);
        }
        finally
        {
            if (_orderIconPresenter != null)
            {
                _orderIconFactory.Release(_orderIconPresenter);
                _orderIconPresenter = null;
            }
        }
    }
}
