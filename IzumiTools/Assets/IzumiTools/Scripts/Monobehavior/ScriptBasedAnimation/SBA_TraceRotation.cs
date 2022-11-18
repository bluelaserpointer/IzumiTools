using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class SBA_TraceRotation : MonoBehaviour
{
    public bool useTransformTarget;
    [SerializeField]
    Transform targetTransform;
    [SerializeField]
    Quaternion targetRotation = Quaternion.identity;
    [Min(0)]
    public float timeLength = 0.1F;
    [Min(0)]
    public float power = 1;
    [SerializeField]
    UnityEvent OnReach;
    //data
    public Quaternion Target => useTransformTarget ? targetTransform.rotation : targetRotation;
    float passedTime = float.MaxValue;
    Quaternion originalRotation;
    public bool AnimationEnd { get; protected set; }
    List<UnityAction> oneTimeReachActions = new List<UnityAction>();
    Quaternion RotationGoal
    {
        get
        {
            Quaternion rotation = Target;
            if (rotation.x == 0 && rotation.y == 0 && rotation.z == 0 && rotation.w == 0)
                rotation = Quaternion.identity;
            return rotation;
        }
    }
    private void FixedUpdate()
    {
        if (AnimationEnd)
            return;
        if (passedTime < timeLength)
        {
            float timeRatio = passedTime / timeLength;
            transform.rotation = Quaternion.Lerp(originalRotation, RotationGoal, Mathf.Pow(timeRatio, power));
            passedTime += Time.fixedDeltaTime;
        }
        else
        {
            EndAnimation();
        }
    }
    public void StartAnimation()
    {
        EndAnimation();
        passedTime = 0;
        originalRotation = transform.rotation;
        AnimationEnd = false;
    }
    public void StopAnimation()
    {
        if (AnimationEnd)
            return;
        AnimationEnd = true;
        passedTime = timeLength;
    }
    public void EndAnimation()
    {
        if (AnimationEnd)
            return;
        AnimationEnd = true;
        passedTime = timeLength;
        transform.rotation = RotationGoal;
        InvokeReachActions();
    }
    public void InvokeReachActions()
    {
        OnReach.Invoke();
        foreach (UnityAction action in oneTimeReachActions)
            OnReach.RemoveListener(action);
        oneTimeReachActions.Clear();
    }
    public void AddReachAction(UnityAction reachAction, bool isOneTime = true)
    {
        if (reachAction == null)
            return;
        OnReach.AddListener(reachAction);
        if (isOneTime)
            oneTimeReachActions.Add(reachAction);
    }
    public void SetTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        useTransformTarget = true;
    }
    public void SetTarget(Quaternion targetRotation)
    {
        this.targetRotation = targetRotation;
        useTransformTarget = false;
    }
}
