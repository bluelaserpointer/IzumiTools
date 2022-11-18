using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    [DisallowMultipleComponent]
    public class ShipMovementController : MonoBehaviour
    {
        //inspector
        [SerializeField]
        Rigidbody _positionRigidbody;
        [SerializeField]
        Rigidbody _rotationRigidbody;
        [SerializeField]
        MouseControllableCamera _mouseControllableCamera;
        [SerializeField]
        ToRotaionByOptimalAccel _rotator;
        [SerializeField]
        ShipMobility _mobility;
        //
        [System.Serializable]
        struct ShipMobility
        {
            public float foward;
            public float backward;
            public float rotateSpeed;
            public float elevation;
            public Vector3 Output(Vector3 movementInput)
            {
                movementInput.z *= (movementInput.z > 0 ? foward : backward);
                movementInput.y *= elevation;
                return movementInput;
            }
        }

        public Rigidbody PositionRigidbody => _positionRigidbody;
        public Rigidbody RotationRigidbody => _rotationRigidbody;
        [HideInInspector]
        public Vector3 movementInput;
        [HideInInspector]
        public Quaternion targetRotation;

        void Update()
        {
            movementInput.z = Input.GetAxisRaw("Vertical");
            movementInput.x = Input.GetAxisRaw("Horizontal");
            targetRotation = _mouseControllableCamera.Camera.transform.rotation;
            _rotator.targetRotation = targetRotation;
        }
        private void FixedUpdate()
        {
            PositionRigidbody.AddForce(RotationRigidbody.transform.TransformVector(_mobility.Output(movementInput)), ForceMode.Force);
        }
    }

}