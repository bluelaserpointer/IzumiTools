using UnityEngine;

[DisallowMultipleComponent]
public class AdditionalBehavior : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
