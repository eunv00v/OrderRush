using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Stove : CookingToolBase
{
    public override string DisplayName => "Stove";

    public override async UniTask InteractAsync(CharacterBase character, CancellationToken ct)
    {
        Debug.Log($"[Stove] InteractAsync 호출됨 - IsHolding: {character.IsHolding}, IsOccupied: {IsOccupied}");

        // 캐릭터가 재료 들고 있으면 올리기
        if (character.IsHolding && !IsOccupied)
        {
            Debug.Log("[Stove] 재료 올리기 시도");
            var ingredientObject = character.PutDown() as IngredientObject;
            if (ingredientObject != null)
            {
                ingredientObject.transform.SetParent(_ingredientSlot);
                ingredientObject.transform.localPosition = Vector3.zero;
                PlaceIngredient(ingredientObject.Context.Data);
            }
        }

        Debug.Log($"[Stove] IsCooking: {IsCooking}, IsOccupied: {IsOccupied}");

        // 재료 없으면 무시
        if (!IsOccupied)
        {
            Debug.Log($"[{DisplayName}] 재료가 없습니다.");
            return;
        }

        // 이미 조리 중이면 무시
        if (IsCooking)
        {
            Debug.Log($"[{DisplayName}] 이미 조리 중입니다.");
            return;
        }

        // CookableAbility 체크
        var cookable = _currentIngredient.Data.GetAbility<CookableAbility>();
        if (cookable == null)
        {
            Debug.Log($"[{DisplayName}] 조리할 수 없는 재료입니다.");
            return;
        }

        Debug.Log($"[Stove] 조리 시작: {cookable.CookDuration}초");
        StartCookingTimer(cookable);
        await UniTask.CompletedTask;
    }
}