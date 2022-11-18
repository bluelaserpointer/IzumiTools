using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Rigidbody based FPS/TPS human movement controller.<br/>
    /// - Accepts physical interaction.<br/>
    /// - Requires curved surface on bottom collider (like capsule) to climb up stairs. 
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class RBBasedHumanMovementController : MonoBehaviour
    {
        //inspector
        [Header("Movement")]
        [Min(0)]
        public float speed = 5f;
        [Min(0)]
        public float jumpHeight = 2f;
        public Cooldown jumpCD = new Cooldown(0.1F);
        public bool controllableInAir = true;
        [Min(0)]
        public float modelRotateLerpFactor = 20F;

        [Header("SE")]
        [SerializeField]
        AudioClip _jumpSE;

        [Header("SelfReference")]
        [SerializeField]
        CollisionChecker _groundChecker;
        [SerializeField]
        Transform _modelYAxis;
        [Tooltip("Warn: CameraYAxis should only do Y rotation, or moving vector could towards sky/ground.")]
        [SerializeField]
        protected Transform _cameraYAxis;
        //--

        //control
        public bool IsGrounded => _groundChecker.CollidingAny;
        public bool IsMoving { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        private Vector3 _inputs = Vector3.zero;
        public Quaternion ModelTargetRotation { get; private set; }

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            //_actionPose = ActionPoseType.Idle;
        }
        void Update()
        {
            IsMoving = false;
            if (IsGrounded || controllableInAir)
            {
                _inputs = Vector3.zero;
                _inputs.x = Input.GetAxis("Horizontal");
                _inputs.z = Input.GetAxis("Vertical");
                if (_inputs != Vector3.zero)
                {
                    IsMoving = true;
                    _inputs = _cameraYAxis.TransformVector(_inputs).normalized;
                    ModelTargetRotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(_inputs.x, _inputs.z), 0);
                }
            }
            jumpCD.AddDeltaTime();
            if (Input.GetButtonDown("Jump") && jumpCD.IsReady)
            {
                if (IsGrounded)
                {
                    jumpCD.Reset();
                    if (_jumpSE != null)
                        OneShotSound.PlayAtAnchor(_jumpSE, transform);
                    Rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                }
                // mid-air jump?
            }
        }
        void FixedUpdate()
        {
            if (_inputs != Vector3.zero)
            {
                Rigidbody.MovePosition(Rigidbody.position + _inputs * speed * Time.fixedDeltaTime);
            }
            _modelYAxis.rotation = Quaternion.LerpUnclamped(_modelYAxis.rotation, ModelTargetRotation, modelRotateLerpFactor * Time.fixedDeltaTime);
        }
    }
}
