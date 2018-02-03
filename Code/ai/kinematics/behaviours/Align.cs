
using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Align : ISteeringBehavior
    {
        protected Kinematic _character;
        protected Kinematic _target;
        private float _maxRotation;

        public Align(Kinematic character, Kinematic target, float maxRotation)
        {
            _character = character;
            _target = target;
            _maxRotation = maxRotation;
        }

        public virtual SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            var rotation = Mathf.Abs(_character.Orientation - _target.Orientation);

            if(rotation > _maxRotation)
            {
                rotation = _maxRotation;
            }

            steering.Rotation = rotation;

            return steering;
        }
    }
}
