using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public enum TargetPriorityType { Nearst, Oldest }
    public string targetsTagName;
    public float detectRange = float.MaxValue;
    public float angleSpeed = 15f;
    public TargetPriorityType targetPriority;
    public Vector3 angleRotation;

    //data
    public bool IsAimed => true;
    GameObject targettingObject;
    public GameObject TargettingObject => targettingObject;

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        LookAtTarget();
    }
    public void UpdateTarget()
    {
        Vector3 myPosition = transform.position;
        if (targetPriority == TargetPriorityType.Oldest && targettingObject != null)
        {
            if (Vector3.Distance(myPosition, targettingObject.transform.position) < detectRange)
                return;
        }
        float candidateDist = detectRange;
        GameObject candidateObject = null;
        foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetsTagName))
        {
            if (!IsVisible(target))
                continue;
            float dist = Vector3.Distance(myPosition, target.transform.position);
            if (dist < candidateDist)
            {
                candidateDist = dist;
                candidateObject = target;
            }
        }
        if (candidateObject != null)
        {
            targettingObject = candidateObject;
        }
    }
    public void LookAtTarget()
    {
        if (targettingObject == null)
            return;
        transform.LookAt(targettingObject.transform);
        transform.Rotate(angleRotation);
    }
    public virtual bool IsVisible(GameObject target)
    {
        return true;
    }
}
