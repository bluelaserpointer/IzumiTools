using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class SBA_Slide : SBA_TracePosition
{
    [SerializeField]
    Transform inPosition, outPosition;
    //data
    bool slidedOut;
    public bool SlidedOut => slidedOut;
    public void SlideOut()
    {
        if (slidedOut)
            return;
        slidedOut = true;
        SetTarget(outPosition);
        StartAnimation();
    }
    public void SlideBack()
    {
        if (!slidedOut)
            return;
        slidedOut = false;
        SetTarget(inPosition);
        StartAnimation();
    }
    public void Switch() {
        if (slidedOut)
            SlideBack();
        else
            SlideOut();
    }
    public void Slide(bool outOrBack)
    {
        if (outOrBack)
            SlideOut();
        else
            SlideBack();
    }
}
