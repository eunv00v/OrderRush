using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WaitForFoodAction : IGameAction
{
    private OrderIconFactory _orderIconFactory;
    private CustomerCharacter _character;
    private OrderIconPresenter _orderIconPresenter;
    public WaitForFoodAction(CustomerCharacter character, OrderIconFactory orderIconFactory)
    {
        _character = character;
        _orderIconFactory = orderIconFactory;
    }
    public async UniTask ExecuteAsync(CancellationToken ct)
    {
        Debug.Log($"[WaitForFoodAction] ExecuteAsync START - Character: {_character.name}, Order: {_character.Order}");

        // null 체크
        if (_character.Order == null)
        {
            Debug.LogError($"[WaitForFoodAction] Order is null! Character: {_character.name}");
            return;
        }

        if (_character.Order.Recipe == null)
        {
            Debug.LogError($"[WaitForFoodAction] Recipe is null! Character: {_character.name}");
            return;
        }

        Debug.Log($"[WaitForFoodAction] Recipe: {_character.Order.Recipe.RecipeName}, Icon: {_character.Order.Recipe.Icon}");

        // 아이콘 생성 및 표시
        _orderIconPresenter = _orderIconFactory.Create(
            _character.transform,
            new Vector3(0, 1.5f, 0),
            _character.Order.Recipe.Icon
        );

        Debug.Log($"[WaitForFoodAction] Icon created: {_orderIconPresenter}");

        try
        {
            // 음식을 받을 때까지 대기 (CancellationToken이 취소될 때까지)
            await UniTask.WaitUntilCanceled(ct);
        }
        finally
        {
            Debug.Log("[WaitForFoodAction] Cleaning up icon");
            // Action 종료 시 자동 정리
            if (_orderIconPresenter != null)
            {
                _orderIconFactory.Release(_orderIconPresenter);
                _orderIconPresenter = null;
            }
        }
    }

}
