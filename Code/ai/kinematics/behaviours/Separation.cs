using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Separation : ISteeringBehavior
    {
        private Kinematic _character;
        private List<Kinematic> _targets;
        private float _maxSpeed;
        private float _treshold;

        public Separation(Kinematic character, List<Kinematic> targets, float maxSpeed, float treshold)
        {
            _character = character;
            _targets = targets;
            _maxSpeed = maxSpeed;
            _treshold = treshold;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            foreach (var target in _targets)
            {
                var direction = _character.Position - target.Position;
                var distance = direction.magnitude;
                if(distance < _treshold)
                {
                    steering.Velocity += direction;
                }
            }

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
