using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class OrderAction : IGameAction
{
    private readonly CustomerCharacter _character;
    private readonly ILevelContextPresenter _levelContext;

    public const float DEFAULT_TIME_LIMIT = 60f;
    public OrderAction(CustomerCharacter character, ILevelContextPresenter levelContextPresenter)
    {
        _character = character;
        _levelContext = levelContextPresenter;
    }

    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        var recipes = _levelContext.CurrentLevelData.AvailableRecipes;
        if (recipes == null || recipes.Count == 0)
        {
            return;
        }

        var recipe = recipes[Random.Range(0, recipes.Count)];
        _character.Order = new Order(recipe, DEFAULT_TIME_LIMIT);
        Debug.Log($"[CustomerCharacter] Order created: {recipe.RecipeName}");


        await UniTask.CompletedTask;
    }
}
