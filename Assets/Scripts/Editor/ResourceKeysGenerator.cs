using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEditor;
public class ResourceKeysGenerator
{
    [MenuItem("Tools/ResourceKeysGenerator/CreatePrefabKeys", priority = 0)]
    public static void CreatePrefabTags()
    {
        EditorUtility.DisplayProgressBar("CreatePrefabKeys", "Working...", 1f);
        Thread.Sleep(1000);

        try
        {
            var path = "Assets/Scripts/Services/ResourceService/PrefabKeys.cs";
            var contents = CreatePrefabKeyContents();
            File.WriteAllText(path, contents);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }

        EditorUtility.ClearProgressBar();
    }

    private static string CreatePrefabKeyContents()
    {
        var assetGuids = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Prefabs", "Assets/Resources" });
        string[] assetPathList = Array.ConvertAll<string, string>(assetGuids, AssetDatabase.GUIDToAssetPath);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("   public static class PrefabKeys");
        sb.AppendLine("   {");
        foreach (var assetPath in assetPathList)
        {
            var fileName = Path.GetFileNameWithoutExtension(assetPath);
            sb.AppendLine($"    public const string {fileName} = \"{fileName}\";");
        }
        sb.AppendLine("");
        sb.AppendLine("    public static Dictionary<string, string> PrefabPaths = new Dictionary<string, string>()");
        sb.AppendLine("    {");
        foreach (var assetPath in assetPathList)
        {
            var fileName = Path.GetFileNameWithoutExtension(assetPath);
            var replacePath = assetPath;
            var removeDirectory = "Assets/Resources/";
            if (assetPath.Contains(removeDirectory))
            {
                var ext = Path.GetExtension(assetPath);
                replacePath = assetPath.Replace(removeDirectory, "");
                replacePath = replacePath.Replace(ext, "");
            }

            sb.AppendLine("        { " + fileName + ", \"" + replacePath + "\" },");
        }
        sb.AppendLine("    };");
        sb.AppendLine("");
        sb.AppendLine("    public static string GetPrefabPath(string tag)");
        sb.AppendLine("    {");
        sb.AppendLine("        if (PrefabPaths.TryGetValue(tag, out var path))");
        sb.AppendLine("        {");
        sb.AppendLine("             return path;");
        sb.AppendLine("         }");
        sb.AppendLine("         return string.Empty;");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}
