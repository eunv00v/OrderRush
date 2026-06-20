using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipesData", menuName = "Order Rush/Recipes Data")]
public class RecipesData : ScriptableObject
{
    [SerializeField] private List<RecipeData> _recipes = new();

    public List<RecipeData> Recipes => _recipes;
}
