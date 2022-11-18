using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TouchToDestroy : MonoBehaviour
{
    public bool touchCollider = true;
    public bool touchTrigger = true;
    public UnityEvent touchEvent;
    private void OnCollisionEnter(Collision collision)
    {
        if(touchCollider && TouchCondition(collision.gameObject))
        {
            Touch();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (touchTrigger && TouchCondition(other.gameObject))
        {
            Touch();
        }
    }
    public void Touch()
    {
        touchEvent.Invoke();
        Destroy(gameObject);
    }
    public virtual bool TouchCondition(GameObject gameObject)
    {
        return true;
    }
}
