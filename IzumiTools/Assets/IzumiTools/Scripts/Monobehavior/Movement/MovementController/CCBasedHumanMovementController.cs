using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// CharacterController based FPS/TPS human movement controller.<br/>
/// - Dont accepts any physical interaction but internal gravity.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class CCBasedHumanMovementController : MonoBehaviour
{
    [Header("Movement")]
    [Min(0)]
    public float moveSpeed = 4.0F;
    [Min(0)]
    public float jumpSpeed = 7.0F;
    public float gravity = 9.8F;
    [Range(0, 1)]
    public float accelLerp = 0.8F;
    public bool controllableInAir = true;
    [SerializeField]
    [Min(0)]
    float modelRotateLerpFactor = 20F;

    [Header("SelfReference")]
    [SerializeField]
    Transform _modelYAxis;
    [Tooltip("Warn: CameraYAxis should only do Y rotation, or moving vector could towards sky/ground.")]
    [SerializeField]
    Transform cameraYAxis;

    [Header("Events")]
    public UnityEvent onActionStateChange;

    //data
    protected CharacterController controller;
    protected Vector3 _moveVector = Vector3.zero;
    public bool IsGrounded { get; protected set;}
    public enum ActionState { Idle, Walk, Run, Jump }
    public ActionState CurrentActionState { get; protected set; }
    public bool LockMovement { get; set; }
    public Quaternion ModelTargetRotation { get; private set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        IsGrounded = controller.isGrounded;
        float xInput = 0, zInput = 0;
        bool jump = false;
        if (!LockMovement)
        {
            xInput = Input.GetAxis("Horizontal");
            zInput = Input.GetAxis("Vertical");
            jump = Input.GetButton("Jump");
        }
        bool hasAction = false;
        if (IsGrounded || controllableInAir)
        {
            if (xInput != 0 || zInput != 0)
            {
                if (IsGrounded)
                {
                    SetModelState(ActionState.Run);
                    hasAction = true;
                }
                Vector3 xzMove = cameraYAxis.TransformVector(new Vector3(xInput, 0, zInput)).normalized * moveSpeed;
                _moveVector.x = Mathf.Lerp(_moveVector.x, xzMove.x, accelLerp);
                _moveVector.z = Mathf.Lerp(_moveVector.z, xzMove.z, accelLerp);
                ModelTargetRotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(xzMove.x, xzMove.z), 0);
            }
            else if (IsGrounded)
            {
                _moveVector.x = Mathf.Lerp(_moveVector.x, 0, accelLerp);
                _moveVector.z = Mathf.Lerp(_moveVector.z, 0, accelLerp);
            }
        }
        if (IsGrounded && jump)
        {
            SetModelState(ActionState.Jump);
            hasAction = true;
            _moveVector.y = jumpSpeed;
        }
        if (!hasAction)
            SetModelState(ActionState.Idle);
        _moveVector.y -= gravity * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        controller.Move(_moveVector * Time.deltaTime);
        _modelYAxis.rotation = Quaternion.LerpUnclamped(_modelYAxis.rotation, ModelTargetRotation, modelRotateLerpFactor * Time.fixedDeltaTime);
    }
    void SetModelState(ActionState modelState)
    {
        if (CurrentActionState.Equals(modelState))
            return;
        CurrentActionState = modelState;
        onActionStateChange.Invoke();
    }
}
