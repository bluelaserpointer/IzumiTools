using UnityEngine;

public class TouchTagToDestroy : TouchToDestroy
{
    public string tagName;
    public override bool TouchCondition(GameObject gameObject)
    {
        return gameObject.tag.Equals(tagName);
    }
}
