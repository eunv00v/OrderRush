using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Order Rush/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string IngredientName;
    public IngredientState InitialState;
    public Sprite Icon;
    public GameObject Prefab;

    [Header("Abilities")]
    public List<IngredientAbility> Abilities = new();

    /// <summary>
    /// 특정 타입의 능력 가져오기
    /// </summary>
    public T GetAbility<T>() where T : IngredientAbility
    {
        return Abilities.OfType<T>().FirstOrDefault();
    }
}
