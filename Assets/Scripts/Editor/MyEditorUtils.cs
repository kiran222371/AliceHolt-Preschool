using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class MyEditorUtils
{
    public static T FindFirstAsset<T>() where T : Object
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        if (guids.Length == 0)
            return null;
        return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[0]));
    }

    public static T[] FindAssets<T>() where T : Object
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] assets = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
        return assets;
    }

    public static void SetPadding(this VisualElement element, int padding)
    {
        element.style.paddingTop = padding;
        element.style.paddingBottom = padding;
        element.style.paddingLeft = padding;
        element.style.paddingRight = padding;
    }

    public static void SetMargin(this VisualElement element, int margin)
    {
        element.style.marginTop = margin;
        element.style.marginBottom = margin;
        element.style.marginLeft = margin;
        element.style.marginRight = margin;
    }
}