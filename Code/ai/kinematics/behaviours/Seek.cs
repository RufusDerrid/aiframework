using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Seek : ISteeringBehavior
    {
        protected Kinematic _character;
        protected Kinematic _target;
        private float _maxSpeed;

        public Seek(Kinematic character, Kinematic target, float maxSpeed)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
        }

        public virtual SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            steering.Velocity = _target.Position - _character.Position;
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
