using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [Min(0)]
    public float timeScale = 1.0f;
    public Vector3 deltaPosition;
    public Vector3 deltaAngle;
    public Vector3 deltaScale;

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaSinTime = Mathf.Sin(Time.timeSinceLevelLoad * timeScale) - Mathf.Sin((Time.timeSinceLevelLoad - Time.deltaTime) * timeScale);
        transform.localPosition += deltaPosition * deltaSinTime;
        transform.localEulerAngles += deltaAngle * deltaSinTime;
        transform.localScale += deltaScale * deltaSinTime;
    }
}
