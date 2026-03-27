using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Order Rush/Recipe")]
public class RecipeData : ScriptableObject
{
    public string RecipeName;
    public List<RecipeIngredient> Ingredients = new();
    public IngredientData ResultItem;  // 추가
}
