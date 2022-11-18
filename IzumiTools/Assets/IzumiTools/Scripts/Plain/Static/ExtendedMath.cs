using UnityEngine;

namespace IzumiTools
{
    public static class ExtendedMath
    {
        /// <summary>
        /// Get signed overflow upon clamping a value. (ex. 5 clampes in [8~10] gets -3)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="overflow">signed overflow value</param>
        /// <returns>Clamped value</returns>
        public static float ClampAndGetOverflow(float value, float min ,float max, out float overflow)
        {
            if(value < min)
            {
                overflow = value - min;
                return min;
            }
            else if(value > max)
            {
                overflow = value - max;
                return max;
            }
            else
            {
                overflow = 0;
                return value;
            }
        }
        /// <summary>
        /// Clamp angle after a rotation. On clamping, the angle will be set to one of the border depends on the rotate direction.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="deltaAngle"></param>
        /// <param name="unsignedMinAngle">reverse-clockwise unsigned min angle[0, 360]</param>
        /// <param name="unsignedMaxAngle">reverse-clockwise unsigned max angle[0, 360]</param>
        /// <returns>Rotated angle</returns>
        public static float RotateWithClamp(float angle, float deltaAngle, float unsignedMinAngle, float unsignedMaxAngle)
        {
            angle += deltaAngle;
            bool lessThenMin = angle < unsignedMinAngle;
            bool biggerThenMax = angle > unsignedMaxAngle;
            if (unsignedMinAngle < unsignedMaxAngle ? (lessThenMin || biggerThenMax) : (lessThenMin && biggerThenMax))
            {
                angle = deltaAngle > 0 ? unsignedMaxAngle : unsignedMinAngle;
            }
            return angle;
        }
        public static bool InFan(Vector2 origin, Vector2 direction, float centralAngle, float radius, Vector2 target)
        {
            Vector2 targetVec = target - origin;
            return targetVec.sqrMagnitude < radius * radius && Vector2.Angle(direction, targetVec) < centralAngle / 2;
        }
        /// <summary>
        /// Get optimal velocity stop at specific distance without overswing.(refering v^2 - v_0^2 = 2*a*x)<br/>
        /// In practice, it should be multiplied by a safe factor (around 0.9) to prevent accumulated errors leading overswing.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="maxVelocityChange"></param>
        /// <returns></returns>
        public static float OptimalVelocityStopAt(float distance, float maxVelocityChange)
        {
            return (distance > 0 ? 1 : -1) * Mathf.Sqrt(2 * maxVelocityChange * Mathf.Abs(distance));
        }
        /// <summary>
        /// Used for deflection shooting, returns the bullet flight time if there exists an aim direction could hit the moving target. 
        /// </summary>
        /// <param name="firePosition"></param>
        /// <param name="bulletSpeed"></param>
        /// <param name="targetPosition"></param>
        /// <param name="targetSpeed"></param>
        /// <returns>The time, negative if impossible to hit the target</returns>
        public static float EstimateBulletFlightTimeOnDeflectionShooting(Vector3 firePosition, float bulletSpeed, Vector3 targetPosition, Vector3 targetSpeed)
        {
            Vector3 deltaPosition = targetPosition - firePosition;
            float a = bulletSpeed - targetSpeed.sqrMagnitude;
            float b = -2 * Vector3.Dot(targetSpeed, deltaPosition);
            float c = -deltaPosition.sqrMagnitude;
            float delta = b * b - 4 * a * c;
            if (delta < 0)
            {
                return -1;
            }
            else if (delta == 0)
            {
                return -b / (2 * a);
            }
            else
            {
                float sqrtDelta = Mathf.Sqrt(delta);
                float bulletFlightTime = (-b - sqrtDelta) / (2 * a);
                if (bulletFlightTime < 0)
                    bulletFlightTime = (-b + sqrtDelta) / (2 * a);
                return bulletFlightTime;
            }
        }
    }
}