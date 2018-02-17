
using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Align : ISteeringBehavior
    {
        protected Agent _character;
        protected Agent _target;
        private float _maxRotation;

        public Align(Agent character, Agent target, float maxRotation)
        {
            _character = character;
            _target = target;
            _maxRotation = maxRotation;
        }

        public virtual SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            var targetY = MapToRange(_target.Transform.rotation.eulerAngles.y);
            var charY = MapToRange(_character.Transform.rotation.eulerAngles.y);
            var rotation = targetY - charY;

            steering.Rotation = (rotation / Mathf.Abs(rotation)) * _maxRotation;

            return steering;
        }

        private float MapToRange(float rotation)
        {
            if (rotation > 180)
            {
                rotation = rotation % 180 - 180;
            }

            return rotation;
        }
    }
}
