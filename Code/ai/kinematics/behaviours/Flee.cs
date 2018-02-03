using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Flee : ISteeringBehavior
    {
        private Kinematic _character;
        private Kinematic _target;
        private float _maxSpeed;

        public Flee(Kinematic character, Kinematic target, float maxSpeed)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
        }

        public SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            steering.Velocity = _character.Position - _target.Position;
            steering.Velocity.Normalize();

            steering.Velocity.x = Mathf.Round(steering.Velocity.x);
            steering.Velocity.y = Mathf.Round(steering.Velocity.y);

            if (steering.Velocity.x != 0)
            {
                steering.Velocity.y = 0;
            }

            steering.Velocity *= _maxSpeed;

            return steering;
        }
    }
}
