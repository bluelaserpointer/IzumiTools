using UnityEngine;

public class KineticMove : MonoBehaviour
{
    public Vector3 positionMove, rotationMove;

    // Update is called once per frame
    void Update()
    {
        transform.position += positionMove * Time.deltaTime;
        transform.Rotate(rotationMove * Time.deltaTime);
    }
}
