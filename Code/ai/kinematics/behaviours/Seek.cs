using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Seek : ISteeringBehavior
    {
        protected Agent _character;
        protected Agent _target;
        private float _maxSpeed;

        public Seek(Agent character, Agent target, float maxSpeed)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
        }

        public virtual SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            var distance = _target.Transform.position - _character.Transform.position;
            steering.Velocity = new Vector3(distance.x, 0, distance.z);
            steering.Velocity.Normalize();

            steering.Velocity *= _maxSpeed;

            return steering;
        }
    }
}
