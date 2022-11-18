using UnityEngine;

[DisallowMultipleComponent]
public class DestroyOnBecameInvisible : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
