using UnityEngine;

public static class RectTransformExtension
{
    public static bool Overlaps(this RectTransform a, RectTransform b)
    {
        return a.WorldRect().Overlaps(b.WorldRect());
    }
    public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse)
    {
        return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
    }

    public static Rect WorldRect(this RectTransform rectTransform)
    {
        float rectTransformWidth = rectTransform.Width();
        float rectTransformHeight = rectTransform.Height();

        Vector3 position = rectTransform.position;
        return new Rect(position.x + rectTransformWidth * rectTransform.pivot.x, position.y - rectTransformHeight * rectTransform.pivot.y, rectTransformWidth, rectTransformHeight);
    }
    public static float Width(this RectTransform rectTransform)
    {
        return rectTransform.sizeDelta.x * rectTransform.lossyScale.x;
    }
    public static float Height(this RectTransform rectTransform)
    {
        return rectTransform.sizeDelta.y * rectTransform.lossyScale.y;
    }
}