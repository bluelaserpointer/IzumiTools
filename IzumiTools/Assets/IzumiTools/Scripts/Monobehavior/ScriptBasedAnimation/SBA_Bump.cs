using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class SBA_Bump : MonoBehaviour
{
    public Transform target;
    [Min(0)]
    public float timeLength;
    [Min(0)]
    public float power = 2;
    [SerializeField]
    UnityEvent OnBump;

    //data
    float passedTime = float.MaxValue;
    Vector3 originalPos;
    bool isBeforeBump;
    List<UnityAction> oneTimeBumpActions = new List<UnityAction>();
    private void FixedUpdate()
    {
        if (passedTime < timeLength)
        {
            float halfTime = timeLength / 2;
            float timePassedRate = 1 - Mathf.Abs(passedTime - halfTime) / halfTime;
            transform.position = Vector3.Lerp(originalPos, target.position, Mathf.Pow(timePassedRate, power));
            passedTime += Time.fixedDeltaTime;
        }
        if (isBeforeBump && passedTime >= timeLength / 2)
        {
            isBeforeBump = false;
            OnBump.Invoke();
            foreach (UnityAction action in oneTimeBumpActions)
                OnBump.RemoveListener(action);
            oneTimeBumpActions.Clear();
        }
    }
    public void StartAnimation()
    {
        passedTime = 0;
        originalPos = transform.position;
        isBeforeBump = true;
    }
    public void AddBumpAction(UnityAction bumpAction, bool isOneTime = true)
    {
        OnBump.AddListener(bumpAction);
        if (isOneTime)
            oneTimeBumpActions.Add(bumpAction);
    }
}
