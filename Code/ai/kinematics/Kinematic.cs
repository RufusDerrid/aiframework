using UnityEngine;

namespace Assets.Code.ai.kinematics
{
    public class Kinematic
    {
        public Vector2 Position;
        public float Orientation;
        public Vector2 Velocity;

        private float _velocityTime;
        private float _rotationTime;

        public void Update(SteeringOutput steering)
        {
            float velocityCellCount = steering.Velocity.magnitude;
            float oneCellVelocityTime = 1 / velocityCellCount;
            _velocityTime += Time.deltaTime;

            if (_velocityTime > oneCellVelocityTime)
            {
                _velocityTime = 0.0f;
                Velocity = steering.Velocity.normalized;
                Position += Velocity;
            }

            Orientation += steering.Rotation;
            if (Orientation > 3)
            {
                Orientation %= 4;
            }

            //float oneTurnTime = 1 / steering.Rotation;
            //_rotationTime += Time.deltaTime;
            //if (_rotationTime > oneTurnTime)
            //{
            //    Orientation += steering.Rotation;
            //    if (Orientation > 3)
            //    {
            //        Orientation %= 3;
            //    }
            //}
        }
    }
}