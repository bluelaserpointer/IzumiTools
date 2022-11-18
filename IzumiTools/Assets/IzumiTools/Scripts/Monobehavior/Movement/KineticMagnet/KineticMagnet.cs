using UnityEngine;

public class KineticMagnet : MonoBehaviour
{
    [SerializeField]
    Transform target;
    public Vector3 finalRotation;
    public float time;

    //data
    Vector3 initialPosition, initialRotation;
    bool isSticked;
    float magnetTime;
    public bool IsSticked => isSticked;
    public Transform Target
    {
        get => target;
        set
        {
            if(target != value)
            {
                target = value;
                isSticked = false;
                initialPosition = target.position;
                initialRotation = target.localEulerAngles;
                magnetTime = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pull();
    }

    public void Pull()
    {
        if (target == null)
        {
            isSticked = false;
            return;
        }
        if (isSticked)
        {
            target.transform.position = transform.position;
        }
        else
        {
            if ((magnetTime += Time.deltaTime) > time)
            {
                isSticked = true;
                target.transform.position = transform.position;
                target.transform.localEulerAngles = finalRotation;
            }
            else
            {
                target.transform.position = Vector3.Lerp(initialPosition, transform.position, magnetTime / time);
                target.transform.localEulerAngles = Vector3.Lerp(initialRotation, finalRotation, magnetTime / time);
            }
        }
    }
}
