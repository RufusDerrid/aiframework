using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Arrive : ISteeringBehavior
    {
        protected Agent _character;
        protected Agent _target;
        private float _maxSpeed;
        private float _radius;

        public Arrive(Agent character, Agent target, float maxSpeed, float radius)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
            _radius = radius;
        }

        public virtual SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            var distance = _target.Transform.position - _character.Transform.position;
            steering.Velocity = new Vector3(distance.x, 0, distance.z);

            if (steering.Velocity.magnitude <= _radius)
            {
                return new SteeringOutput();
            }

            if (steering.Velocity.magnitude > _maxSpeed)
            {
                steering.Velocity.Normalize();
                steering.Velocity *= _maxSpeed;
            }

            return steering;
        }
    }
}