using UnityEngine;

/// <summary>
/// 조리 가능 능력
/// 재료를 조리할 수 있는 능력 (굽기, 삶기 등)
/// </summary>
[CreateAssetMenu(fileName = "New Cookable Ability", menuName = "Order Rush/Abilities/Cookable")]
public class CookableAbility : IngredientAbility
{
    [Tooltip("조리 시간 (초)")]
    public float cookDuration;

    [Tooltip("조리 후 상태")]
    public IngredientState resultState = IngredientState.Cooked;
}
