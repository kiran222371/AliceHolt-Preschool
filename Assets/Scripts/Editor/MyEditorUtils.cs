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

    public static void SetPadding(this VisualElement element, Length length)
    {
        element.style.paddingTop = length;
        element.style.paddingBottom = length;
        element.style.paddingLeft = length;
        element.style.paddingRight = length;
    }

    public static void SetMargin(this VisualElement element, Length length)
    {
        element.style.marginTop = length;
        element.style.marginBottom = length;
        element.style.marginLeft = length;
        element.style.marginRight = length;
    }

    public static void SetBorderRadius(this VisualElement element, Length length)
    {
        element.style.borderTopLeftRadius = length;
        element.style.borderTopRightRadius = length;
        element.style.borderBottomLeftRadius = length;
        element.style.borderBottomRightRadius = length;
    }
}