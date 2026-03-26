using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Order Rush/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public IngredientState initialState;
    public Sprite icon;
    public GameObject prefab;

    [Header("Abilities")]
    public List<IngredientAbility> abilities = new();

    /// <summary>
    /// 특정 타입의 능력 가져오기
    /// </summary>
    public T GetAbility<T>() where T : IngredientAbility
    {
        return abilities.OfType<T>().FirstOrDefault();
    }
}
