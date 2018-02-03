using UnityEngine;

namespace Assets.Code.ai.kinematics
{
    public class KinematicUtils
    {
        public static float GetOrientationFromVelocity(float currentOrientation, Vector2 velocity)
        {
            if (velocity.sqrMagnitude > 0)
            {
                return Mathf.Atan2(-velocity.y, velocity.x);
            }
            else
            {
                return currentOrientation;
            }
        }

        public static Vector2 VectorFromAngle(float angleInRad)
        {
            float x = Mathf.Cos(angleInRad);
            float y = Mathf.Sin(angleInRad);

            return new Vector2(x, -y);
        }
    }
}
