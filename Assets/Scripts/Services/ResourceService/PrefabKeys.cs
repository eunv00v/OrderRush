using System;
using System.Collections.Generic;
   public static class PrefabKeys
   {
    public const string DefaultCube = "DefaultCube";
    public const string Steak = "Steak";
    public const string SteakMeat = "SteakMeat";
    public const string KithenTalbe = "KithenTalbe";
    public const string Plate = "Plate";
    public const string PlateRack = "PlateRack";
    public const string Player = "Player";
    public const string ProjectLifetimeScope = "ProjectLifetimeScope";
    public const string MeatFridge = "MeatFridge";
    public const string TestCube = "TestCube";
    public const string Stove = "Stove";

    public static Dictionary<string, string> PrefabPaths = new Dictionary<string, string>()
    {
        { DefaultCube, "Assets/Prefabs/DefaultCube.prefab" },
        { Steak, "Assets/Prefabs/Ingredients/Steak.prefab" },
        { SteakMeat, "Assets/Prefabs/Ingredients/SteakMeat.prefab" },
        { KithenTalbe, "Assets/Prefabs/KithenTalbe.prefab" },
        { Plate, "Assets/Prefabs/Plate/Plate.prefab" },
        { PlateRack, "Assets/Prefabs/Plate/PlateRack.prefab" },
        { Player, "Assets/Prefabs/Player.prefab" },
        { ProjectLifetimeScope, "Assets/Prefabs/ProjectLifetimeScope.prefab" },
        { MeatFridge, "Assets/Prefabs/Storage/MeatFridge.prefab" },
        { TestCube, "Assets/Prefabs/TestCube.prefab" },
        { Stove, "Assets/Prefabs/Tools/Stove.prefab" },
    };

    public static string GetPrefabPath(string tag)
    {
        if (PrefabPaths.TryGetValue(tag, out var path))
        {
             return path;
         }
         return string.Empty;
    }
}
