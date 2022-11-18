using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Head towards position by optimal accel, without overswing in most cases.<br/>
    /// Top accel is limitable, while it's change speed is not limitable. <br/>
    /// If you lower accel limit during its operation, its could still overswing.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class ToPositionByOptimalAccel : MonoBehaviour
    {
        //inspector
        public Vector3 targetPosition;
        public float maxVelocityChange;
        public float safeStability = 0.9F;
        //

        public Rigidbody Rigidbody { get; private set; }
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float deltaMaxVelocityChange = Time.fixedDeltaTime * maxVelocityChange;
            Vector3 deltaPosition = targetPosition - Rigidbody.position;
            if (deltaPosition == Vector3.zero)
                return;
            Vector3 optimalVelocity = new Vector3(OptimalVelocityStopAt(deltaPosition.x), OptimalVelocityStopAt(deltaPosition.y), OptimalVelocityStopAt(deltaPosition.z));
            Vector3 actualVelocity = Vector3.MoveTowards(Rigidbody.velocity, optimalVelocity, maxVelocityChange * Time.fixedDeltaTime);
            if (Mathf.Abs(deltaPosition.magnitude) < 0.005F && Mathf.Abs(actualVelocity.magnitude) < deltaMaxVelocityChange)
            {
                Rigidbody.position = targetPosition;
                Rigidbody.velocity = Vector3.zero;
            }
            else
            {
                Rigidbody.velocity = actualVelocity;
            }
        }
        private float OptimalVelocityStopAt(float distance)
        {
            return ExtendedMath.OptimalVelocityStopAt(distance, maxVelocityChange) * safeStability;
        }
    }
}