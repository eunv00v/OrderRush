using UnityEngine;

/// <summary>
/// 썰기 가능 능력
/// 재료를 썰 수 있는 능력
/// </summary>
[CreateAssetMenu(fileName = "New Sliceable Ability", menuName = "Order Rush/Abilities/Sliceable")]
public class SliceableAbility : IngredientAbility
{
    [Tooltip("썰기 시간 (초)")]
    public float sliceDuration;

    [Tooltip("썰기 후 상태")]
    public IngredientState resultState = IngredientState.Processed;
}
