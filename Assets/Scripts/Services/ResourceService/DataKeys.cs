using System;
using System.Collections.Generic;

public static class DataKeys
{
    public const string TableEffect = "TableEffect";
    public const string CardsData = "CardsData";
    public const string TableCard = "TableCard";
    public const string GameConfig = "GameConfig";
    public const string GrilledMushroom = "GrilledMushroom";
    public const string Mushroom = "Mushroom";
    public const string Onion = "Onion";
    public const string OnionRing = "OnionRing";
    public const string SlicedOnion = "SlicedOnion";
    public const string Steak = "Steak";
    public const string SteakMeat = "SteakMeat";
    public const string Recipe01_Steak = "Recipe01_Steak";
    public const string Recipe02_OnionRingSteak = "Recipe02_OnionRingSteak";
    public const string RecipesData = "RecipesData";
    public const string Run1_Days = "Run1_Days";
    public const string RunsData = "RunsData";

    public static Dictionary<string, string> DataPaths = new Dictionary<string, string>()
    {
        { TableEffect, "Assets/Data/Card/CardEffect/TableEffect.asset" },
        { CardsData, "Assets/Data/Card/CardsData.asset" },
        { TableCard, "Assets/Data/Card/TableCard.asset" },
        { GameConfig, "Assets/Data/GameConfig.asset" },
        { GrilledMushroom, "Assets/Data/Ingredient/Mushroom/GrilledMushroom.asset" },
        { Mushroom, "Assets/Data/Ingredient/Mushroom/Mushroom.asset" },
        { Onion, "Assets/Data/Ingredient/Onion/Onion.asset" },
        { OnionRing, "Assets/Data/Ingredient/Onion/OnionRing.asset" },
        { SlicedOnion, "Assets/Data/Ingredient/Onion/SlicedOnion.asset" },
        { Steak, "Assets/Data/Ingredient/Steak/Steak.asset" },
        { SteakMeat, "Assets/Data/Ingredient/Steak/SteakMeat.asset" },
        { Recipe01_Steak, "Assets/Data/Recipe/Recipe01_Steak.asset" },
        { Recipe02_OnionRingSteak, "Assets/Data/Recipe/Recipe02_OnionRingSteak.asset" },
        { RecipesData, "Assets/Data/Recipe/RecipesData.asset" },
        { Run1_Days, "Assets/Data/Run1_Days.asset" },
        { RunsData, "Assets/Data/RunsData.asset" },
    };

    public static string GetDataPath(string tag)
    {
        if (DataPaths.TryGetValue(tag, out var path))
        {
            return path;
        }
        return string.Empty;
    }
}
