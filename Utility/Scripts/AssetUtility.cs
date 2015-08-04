#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class AssetUtility {

    public static void CreateAssetInSelectedDirectory(Object obj, string name = null) {
        var dir = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (!System.IO.Directory.Exists(dir))
            dir = "Assets";

        if (string.IsNullOrEmpty(name))
            name = obj.GetType().ToString();

        var path = string.Concat(dir, "/", name, ".asset");

        AssetDatabase.CreateAsset(obj, path);
    }


    public static bool SetTextureReadability(string texPath, bool readable) {
        var importer = AssetImporter.GetAtPath(texPath) as TextureImporter;
        if (importer == null) return false;

        if (importer.isReadable == readable)
            return readable;

        importer.isReadable = readable;
        AssetDatabase.ImportAsset(texPath);

        return !readable;
    }

    public static bool SetTextureReadability(Texture2D tex, bool readable) {
        return SetTextureReadability(AssetDatabase.GetAssetPath(tex), readable);
    }


    public static bool HasLabel(Object obj, string label) {
        return ArrayUtility.IndexOf(AssetDatabase.GetLabels(obj), label) != -1;
    }

    public static bool HasLabel(string assetPath, string label) {
        return HasLabel(AssetDatabase.LoadMainAssetAtPath(assetPath), label);
    }


    public static void AddLabel(Object obj, string label) {
        var labels = AssetDatabase.GetLabels(obj);

        if (ArrayUtility.IndexOf(labels, label) != -1) return;

        ArrayUtility.Add(ref labels, label);
        AssetDatabase.SetLabels(obj, labels);
    }

    public static void AddLabel(string assetPath, string label) {
        AddLabel(AssetDatabase.LoadMainAssetAtPath(assetPath), label);
    }

    public static void AddLabels(Object obj, params string[] labels) {
        foreach (var label in labels)
            AddLabel(obj, label);
    }

    public static void AddLabels(string assetPath, params string[] labels) {
        AddLabels(AssetDatabase.LoadMainAssetAtPath(assetPath), labels);
    }


    public static void RemoveLabel(Object obj, string label) {
        var labels = AssetDatabase.GetLabels(obj);

        ArrayUtility.Remove(ref labels, label);
        AssetDatabase.SetLabels(obj, labels);
    }

    public static void RemoveLabel(string assetPath, string label) {
        RemoveLabel(AssetDatabase.LoadMainAssetAtPath(assetPath), label);
    }

    public static void RemoveLabels(Object obj, params string[] labels) {
        foreach (var label in labels)
            RemoveLabel(obj, label);
    }

    public static void RemoveLabels(string assetPath, params string[] labels) {
        RemoveLabels(AssetDatabase.LoadMainAssetAtPath(assetPath), labels);
    }
}
#endif
