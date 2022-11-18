using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Head towards rotation by optimal accel, without overswing in most cases.<br/>
    /// Top accel is limitable, while it's change speed is not limitable. <br/>
    /// If you lower accel limit during its operation, its could still overswing.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class ToRotaionByOptimalAccel : MonoBehaviour
    {
        //inspector
        public Quaternion targetRotation;
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
            Quaternion deltaRotation = targetRotation * Quaternion.Inverse(Rigidbody.rotation);
            Vector3 deltaEularAngles = (deltaRotation.w > 0 ? 1 : -1) * new Vector3(deltaRotation.x, deltaRotation.y, deltaRotation.z);
            Vector3 optimalAngVelocity = new Vector3(OptimalVelocityStopAt(deltaEularAngles.x), OptimalVelocityStopAt(deltaEularAngles.y), OptimalVelocityStopAt(deltaEularAngles.z));
            Vector3 actualAngVelocity = Vector3.MoveTowards(Rigidbody.angularVelocity, optimalAngVelocity, maxVelocityChange * Time.fixedDeltaTime);
            Rigidbody.angularVelocity = actualAngVelocity;
            //Rigidbody.AddTorque((realAngVelocity - Rigidbody.angularVelocity) / Time.fixedDeltaTime, ForceMode.Acceleration); //Same effects?
        }
        private float OptimalVelocityStopAt(float distance)
        {
            return ExtendedMath.OptimalVelocityStopAt(distance, maxVelocityChange) * safeStability;
        }
    }
}