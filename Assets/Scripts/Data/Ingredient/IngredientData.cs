using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class IngredientVisual
{
    public IngredientState State;
    public Material Material;
}


[CreateAssetMenu(fileName = "New Ingredient", menuName = "Order Rush/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string IngredientName;
    public IngredientState InitialState;
    public Sprite Icon;
    public GameObject Prefab;

    [Header("Abilities")]
    public List<IngredientAbility> Abilities = new();

    [Header("Materials")]
    public List<IngredientVisual> Visuals = new();

    public T GetAbility<T>() where T : IngredientAbility
    {
        return Abilities.OfType<T>().FirstOrDefault();
    }

    public Material GetMaterial(IngredientState state)
    {
        return Visuals.FirstOrDefault(v => v.State == state)?.Material;
    }

}
