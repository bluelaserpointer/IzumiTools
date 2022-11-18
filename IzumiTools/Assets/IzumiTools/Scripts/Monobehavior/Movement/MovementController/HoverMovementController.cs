using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D hover movement controller usually used for free camera.<br/>
/// <br/>
/// Caution: Due to its 3D movement, Please configure a new input axis in playersettings for "yAxisInputName" in the inspector.
/// </summary>
[DisallowMultipleComponent]
public class HoverMovementController : MonoBehaviour
{
    //inspector
    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 5F;
    [SerializeField]
    [Tooltip(
        "- Horizon: Y world space, X, Z local space.\r\n" +
        "- Local: All local space.\r\n" +
        "- World: All world space."
        )]
    MovementType _movementType = MovementType.Horizon;

    [Header("Input")]
    [SerializeField]
    string xAxisInputName = "Horizontal";
    [SerializeField]
    string yAxisInputName = "(add this from playersettings!)";
    [SerializeField]
    string zAxisInputName = "Vertical";

    //data
    enum MovementType { Local, World, Horizon }

    void Update()
    {
        float xAxisInput = Input.GetAxis(xAxisInputName);
        float yAxisInput = Input.GetAxis(yAxisInputName);
        float zAxisInput = Input.GetAxis(zAxisInputName);
        Vector3 inputVec;
        switch(_movementType)
        {
            case MovementType.Local:
                inputVec = transform.TransformVector(new Vector3(xAxisInput, yAxisInput, zAxisInput));
                break;
            case MovementType.World:
                inputVec = new Vector3(xAxisInput, yAxisInput, zAxisInput);
                break;
            case MovementType.Horizon:
                inputVec = xAxisInput * transform.right;
                inputVec += yAxisInput * Vector3.up;
                inputVec += zAxisInput * Vector3.Cross(transform.right, Vector3.up).normalized;
                break;
            default:
                inputVec = Vector3.zero;
                break;
        }
        transform.Translate(inputVec * moveSpeed * Time.deltaTime, Space.World);
    }
}
