using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBA_FadeIO : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    float alphaChangePerSec = 1;
    [Range(0, 1)]
    public float targetAlpha;

    private void FixedUpdate()
    {
        if(canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, alphaChangePerSec * Time.fixedDeltaTime);
        }
    }
    public void FadeIn()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        targetAlpha = 1;
    }
    public void FadeOut()
    {
        canvasGroup.interactable = false;
        targetAlpha = 0;
    }
}
