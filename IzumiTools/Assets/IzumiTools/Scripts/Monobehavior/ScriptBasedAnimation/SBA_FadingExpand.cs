using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBA_FadingExpand : MonoBehaviour
{
    public float expandRate = 1.2F;
    public CanvasGroup fadingGroup;
    [Min(0)]
    public float timeLength = 0.1F;
    [Min(0)]
    public float power = 1;
    //data
    float passedTime = float.MaxValue;
    bool isFinished = true;
    Vector3 originalScale;
    public void StartAnimation()
    {
        if (!isFinished)
            Finish();
        gameObject.SetActive(true);
        passedTime = 0;
        originalScale = transform.localScale;
        isFinished = false;
    }
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (isFinished)
            return;
        if (passedTime < timeLength)
        {
            float timePassedRate = passedTime / timeLength;
            float rate = Mathf.Pow(timePassedRate, power);
            transform.localScale = Vector3.Lerp(originalScale, originalScale * expandRate, rate);
            fadingGroup.alpha = 1 - rate;
            passedTime += Time.fixedDeltaTime;
        }
        else
        {
            Finish();
        }
    }
    public void Finish()
    {
        if(!isFinished)
        {
            isFinished = true;
            gameObject.SetActive(false);
            transform.localScale = originalScale;
            fadingGroup.alpha = 1;
            //TODO: multiple time expand
        }
    }
    private void OnEnable()
    {
        Finish();
    }
}
