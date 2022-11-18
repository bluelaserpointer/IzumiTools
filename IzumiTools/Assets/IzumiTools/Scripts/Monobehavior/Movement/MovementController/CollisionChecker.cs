using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Observes all colliding objects not in ignore list.<br/>
/// Requires own collider isTrigger = true.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CollisionChecker : MonoBehaviour
{
    public List<Collider> ignoreList;

    [HideInInspector]
    public List<Collider> collidingList = new List<Collider>();

    public bool CollidingAny => collidingList.Count > 0;
    private void OnTriggerEnter(Collider other)
    {
        if (!ignoreList.Contains(other))
        {
            collidingList.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!ignoreList.Contains(other))
        {
            collidingList.Remove(other);
        }
    }
}
