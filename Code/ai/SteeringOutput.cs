using UnityEngine;

namespace Assets.Code.ai
{
    public class SteeringOutput
    {
        public Vector3 Velocity;
        public float Rotation;

        public SteeringOutput()
        {
            Velocity = Vector3.zero;
            Rotation = 0.0f;
        }

        public SteeringOutput(Vector3 velocity, float rotation)
        {
            Velocity = velocity;
            Rotation = rotation;
        }

        public void Clear()
        {
            Velocity = Vector3.zero;
            Rotation = 0;
        }

        public static SteeringOutput operator +(SteeringOutput steer1, SteeringOutput steer2)
        {
            return new SteeringOutput(steer1.Velocity + steer2.Velocity, steer1.Rotation + steer2.Rotation);
        }

        public static SteeringOutput operator *(SteeringOutput steer, float f)
        {
            return new SteeringOutput(steer.Velocity * f, steer.Rotation * f);
        }

        public static SteeringOutput operator *(float f, SteeringOutput steer)
        {
            return new SteeringOutput(steer.Velocity * f, steer.Rotation * f);
        }

        public static bool operator ==(SteeringOutput steer1, SteeringOutput steer2)
        {
            return steer1.Velocity == steer2.Velocity &&
                steer1.Rotation == steer2.Rotation;
        }

        public static bool operator !=(SteeringOutput steer1, SteeringOutput steer2)
        {
            return steer1.Velocity != steer2.Velocity ||
                steer1.Rotation != steer2.Rotation;
        }

        public float SquareMagnitude()
        {
            return Velocity.sqrMagnitude + Rotation * Rotation;
        }

        public float Magnitude()
        {
            return Mathf.Sqrt(SquareMagnitude());
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (SteeringOutput)obj;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
