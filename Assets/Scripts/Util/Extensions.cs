using UnityEngine;
using UnityEngine.UIElements;

public static class Extensions
{
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
    public static void SetBorderColor(this VisualElement element, Color color)
    {
        element.style.borderTopColor = color;
        element.style.borderBottomColor = color;
        element.style.borderLeftColor = color;
        element.style.borderRightColor = color;
    }

    public static void Zero(this ref Vector3 vector)
    {
        vector.x = 0;
        vector.y = 0;
        vector.z = 0;
    }
}